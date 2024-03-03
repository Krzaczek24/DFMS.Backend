using AutoMapper;
using Core.Database.Services;
using DFMS.Database.Dto.Users;
using DFMS.Database.Exceptions;
using DFMS.Database.Models;
using DFMS.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IUserService
    {
        public Task UpdateLastLoginDate(string login);
        public Task RemoveRefreshTokens(string login, string clientIp);
        public Task SaveNewRefreshToken(string login, string clientIp, string refreshToken, DateTime? validUntil);
        public Task<bool> UpdateRefreshToken(string login, string clientIp, string newRefreshToken, DateTime? validUntil);
        public Task<bool> AuthenticateUser(string login, string passwordHash);
        public Task<User> GetUser(int id);
        public Task<User> GetUser(string login);
        public Task<User> GetUser(string refreshToken, string clientIp);
        public Task<int> GetUsersCount(string phraseFilter, DateTime? onlineAfter = null, bool? isActive = true);
        public Task<IEnumerable<User>> SearchUsers(string phraseFilter = null, DateTime? onlineAfter = null, bool? isActive = true, int page = 0, int pageSize = 10);
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

        public async Task RemoveRefreshTokens(string login, string clientIp)
        {
            var sessions = await (from us in Database.UserSessions
                                  join u in Database.Users on us.User.Id equals u.Id
                                  where u.Login == login && u.Active.Value
                                  select us).ToListAsync();

            if (!string.IsNullOrEmpty(clientIp))
                sessions = sessions.Where(session => session.ClientIp == clientIp).ToList();

            sessions.ForEach(session => Database.Remove(session));
            await Database.SaveChangesAsync();
        }

        public async Task SaveNewRefreshToken(string login, string clientIp, string refreshToken, DateTime? validUntil)
        {
            if (await UpdateRefreshToken(login, clientIp, refreshToken, validUntil))
                return;

            var newRefreshToken = new DbUserSession()
            {
                AddLogin = login,
                User = await Database.Users.Where(u => u.Login == login && u.Active.Value).SingleAsync(),
                RefreshToken = refreshToken,
                ClientIp = clientIp,
                ValidUntil = validUntil
            };

            try
            {
                await Database.AddAsync(newRefreshToken);
                await Database.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.StartsWith("Duplicate entry") ?? false)
            {
                throw new DuplicatedEntryException($"User with login [{login}] already has session on this machine [{clientIp}]", ex);
            }
        }

        public async Task<bool> UpdateRefreshToken(string login, string clientIp, string newRefreshToken, DateTime? validUntil)
        {
            var sessions = await (from us in Database.UserSessions
                                  join u in Database.Users on us.User.Id equals u.Id
                                  where u.Login == login && u.Active.Value
                                  && us.ClientIp == clientIp
                                  select us).ToListAsync();

            foreach (var session in sessions)
            {
                session.ValidUntil = validUntil;
                session.RefreshToken = newRefreshToken;
            }

            return await Database.SaveChangesAsync() > 0;
        }

        public async Task<bool> AuthenticateUser(string login, string passwordHash)
        {
            var count = Database.Users
                .Where(u => u.Login == login && u.PasswordHash == passwordHash)
                .CountAsync();

            return await count > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var query = from u in Database.Users
                        where u.Id == id
                        join r in Database.UserRoles on u.Role.Id equals r.Id
                        select new User()
                        {
                            Id = u.Id,
                            Login = u.Login,
                            Role = Enum.Parse<UserRole>(r.Name, true),
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

        public async Task<User> GetUser(string login)
        {
            var query = from u in Database.Users
                        where u.Login == login
                        join r in Database.UserRoles on u.Role.Id equals r.Id
                        select new User()
                        {
                            Id = u.Id,
                            Login = u.Login,
                            Role = Enum.Parse<UserRole>(r.Name, true),
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

        public async Task<User> GetUser(string refreshToken, string clientIp)
        {
            var query = from u in Database.Users
                        join s in Database.UserSessions on u.Id equals s.User.Id
                        join r in Database.UserRoles on u.Role.Id equals r.Id
                        where s.RefreshToken == refreshToken && s.ClientIp == clientIp && s.ValidUntil > DateTime.Now
                        select new User()
                        {
                            Id = u.Id,
                            Login = u.Login,
                            Role = Enum.Parse<UserRole>(r.Name, true),
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

        public async Task<int> GetUsersCount(string phraseFilter, DateTime? onlineAfter = null, bool? isActive = true)
        {
            return await GetUserSearchQuery(phraseFilter, onlineAfter, isActive).CountAsync();
        }

        public async Task<IEnumerable<User>> SearchUsers(string phraseFilter = null, DateTime? onlineAfter = null, bool? isActive = true, int page = 0, int pageSize = 10)
        {
            var result = await GetUserSearchQuery(phraseFilter, onlineAfter, isActive)
                .Skip(page * pageSize).Take(pageSize).ToListAsync();

            return Mapper.Map<IEnumerable<User>>(result);
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
                            Name = r.Name,
                            Level = r.Level
                        };

            var roles = await query.ToArrayAsync();
            return roles;
        }

        private IQueryable<DbUser> GetUserSearchQuery(string phraseFilter, DateTime? onlineAfter = null, bool? isActive = true)
        {
            const StringComparison comparison = StringComparison.InvariantCultureIgnoreCase;

            var query = Database.Users.AsQueryable();

            if (onlineAfter.HasValue)
                query = query.Where(u => u.LastLoginDate != null && u.LastLoginDate > onlineAfter.Value);

            if (isActive.HasValue)
                query = query.Where(u => u.Active.Value == isActive.Value);

            if (!string.IsNullOrEmpty(phraseFilter))
                query = query.Where(u => u.Login.Contains(phraseFilter, comparison)
                                      || (u.FirstName != null && u.FirstName.Contains(phraseFilter, comparison))
                                      || (u.LastName != null && u.LastName.Contains(phraseFilter, comparison))
                                      || (u.Email != null && u.Email.Contains(phraseFilter, comparison)));

            return query;
        }
    }
}
