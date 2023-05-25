﻿using AutoMapper;
using Core.Database.Services;
using DFMS.Database.Dto.Users;
using DFMS.Database.Exceptions;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IUserService
    {
        public Task UpdateLastLoginDate(string login);
        public Task<bool> AuthenticateUser(string login, string passwordHash);
        public Task<User> GetUser(string login);
        public Task<User> CreateUser(string addLogin, string login, string passwordHash, string email = null, string firstName = null, string lastName = null);
        public Task<Role[]> GetRoles();
    }

    public class UserService : DbService<AppDbContext>, IUserService
    {
        public UserService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public async Task UpdateLastLoginDate(string login)
        {
            await Database.Users
                .Where(u => u.Login == login)
                .ExecuteUpdateAsync(q => q.SetProperty(p => p.LastLoginDate, DateTime.Now));
        }

        public async Task<bool> AuthenticateUser(string login, string passwordHash)
        {
            var count = Database.Users
                .Where(u => u.Login == login && u.PasswordHash == passwordHash)
                .CountAsync();

            return await count > 0;
        }

        public async Task<User> GetUser(string login)
        {
            var query = from u in Database.Users
                        where u.Login == login
                        join r in Database.UserRoles on u.Role.Id equals r.Id
                        select new User()
                        {
                            Id = u.Id,
                            Login = u.Login,
                            Role = r.Name,
                            Permissions = (from upga in Database.UserPermissionGroupAssignments
                                           join upg in Database.UserPermissionGroups on upga.PermissionGroup.Id equals upg.Id
                                           join upa in Database.UserPermissionAssignments on upg.Id equals upa.PermissionGroup.Id
                                           join up in Database.UserPermissions on upa.Permission.Id equals up.Id
                                           where upga.User.Id == u.Id
                                           && upga.Active.Value && upg.Active.Value && upa.Active.Value && up.Active.Value
                                           && (upga.ValidUntil == null || upga.ValidUntil > DateTime.Now)
                                           select up.Name).AsEnumerable().ToArray(),
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            LastLogin = u.LastLoginDate,
                            CreatedAt = u.AddDate
                        };

            var user = await query.SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> CreateUser(string addLogin, string login, string passwordHash, string email = null, string firstName = null, string lastName = null)
        {
            var newUser = new DbUser()
            {
                AddLogin = addLogin,
                Login = login,
                PasswordHash = passwordHash,
                Role = await Database.UserRoles.SingleAsync(role => role.Level == default),
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            try
            {
                Database.Attach(newUser.Role);
                await Database.AddAsync(newUser);
                await Database.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.StartsWith("Duplicate entry") ?? false)
            {
                throw new DuplicatedEntryException($"User with login [{login}] already exists", ex);
            }

            var user = Mapper.Map<User>(newUser);
            return user;
        }

        public async Task<Role[]> GetRoles()
        {
            var query = from r in Database.UserRoles
                        select new Role()
                        {
                            Id = r.Id,
                            Name = r.Name
                        };

            var roles = await query.ToArrayAsync();
            return roles;
        }
    }
}
