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
            var query = from u in Database.Users
                        join r in Database.UserRoles on u.Role.Id equals r.Id
                        from upga in Database.UserPrivilegeGroupAssignments.Where(x => x.User.Id == u.Id).DefaultIfEmpty()
                        from upg in Database.UserPrivilegeGroups.Where(x => x.Id == upga.PrivilegeGroup.Id).DefaultIfEmpty()
                        from upa in Database.UserPrivilegeAssignments.Where(x => x.PrivilegeGroup.Id == upg.Id).DefaultIfEmpty()
                        from up in Database.UserPrivileges.Where(x => x.Id == upa.Privilege.Id).DefaultIfEmpty()
                        select new UserRow()
                        {
							Id = u.Id,
							Login = u.Login,
							Role = r.Name,
							Privilege = up.Name,
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
    }
}
