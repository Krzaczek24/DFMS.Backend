using AutoMapper;
using DFMS.Database.Dto.Users;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string login, string passwordHash);
        public Task<User> CreateUser(string login, string passwordHash, int roleId, string firstName = null, string lastName = null);
        public Task<Role[]> GetRoles();
    }

    public class UserService : DbService, IUserService
    {
        public UserService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public async Task<User> GetUser(string login, string passwordHash)
        {
            var query = from u in Database.Users
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
                            LastName = u.LastName
                        };

            var user = await query.SingleOrDefaultAsync();
            return user;
        }

        public async Task<User> CreateUser(string login, string passwordHash, int roleId, string firstName = null, string lastName = null)
        {
            var newUser = new DbUser()
            {
                Login = login,
                PasswordHash = passwordHash,
                Role = new DbUserRole() { Id = roleId },
                FirstName = firstName,
                LastName = lastName
            };
            Database.Attach(newUser.Role);
            await Database.AddAsync(newUser);
            await Database.SaveChangesAsync();

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
