using AutoMapper;
using DFMS.Database.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string login, string passwordHash);
    }

    public class UserService : DbService, IUserService
    {
        public UserService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public async Task<User> GetUser(string login, string passwordHash)
        {
            try
            {
                var query = from u in Database.Users
                            join r in Database.UserRoles on u.Role.Id equals r.Id
                            from upga in Database.UserPermissionGroupAssignments.Where(x => x.User.Id == u.Id).DefaultIfEmpty()
                            from upg in Database.UserPermissionGroups.Where(x => x.Id == upga.PermissionGroup.Id).DefaultIfEmpty()
                            from upa in Database.UserPermissionAssignments.Where(x => x.PermissionGroup.Id == upg.Id).DefaultIfEmpty()
                            from up in Database.UserPermissions.Where(x => x.Id == upa.Permission.Id).DefaultIfEmpty()
                            select new UserRow()
                            {
                                Id = u.Id,
                                Login = u.Login,
                                Role = r.Name,
                                Permission = up.Name,
                                FirstName = u.FirstName,
                                LastName = u.LastName
                            };

                var dbUser = await query.ToListAsync();
                if (dbUser?.Count > 0)
                {
                    var user = Mapper.Map<User>(dbUser);
                    return user;
                }

                return null;
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }

            return null;
        }
    }
}
