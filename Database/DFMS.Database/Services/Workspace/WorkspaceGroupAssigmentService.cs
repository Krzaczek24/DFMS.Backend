using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Workspace
{
    public interface IWorkspaceGroupAssigmentService
    {
        Task AddGroupMember(string creatorLogin, int groupId, int userId);
        Task<bool> RemoveGroupMember(int groupId, int userId);
    }

    public class WorkspaceGroupAssigmentService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IWorkspaceGroupAssigmentService
    {
        public async Task AddGroupMember(string creatorLogin, int groupId, int userId)
        {
            try
            {
                var newAssigment = new DbUserGroupMember()
                {
                    AddLogin = creatorLogin,
                    Group = await Database.UserGroups.FindAsync(groupId),
                    User = await Database.Users.FindAsync(userId)
                };

                await Database.AddAsync(newAssigment);
                await Database.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> RemoveGroupMember(int groupId, int userId)
        {
            try
            {
                var assignment = await Database.UserGroupMembers
                    .ActiveWhere(x => x.Group.Id == groupId && x.User.Id == userId)
                    .SingleOrDefaultAsync();

                if (assignment == null)
                    return false;

                Database.Remove(assignment);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateException())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
    }
}
