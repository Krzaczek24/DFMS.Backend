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
    public interface IWorkspaceGroupService
    {
        Task<bool> DoesGroupExist(int workspaceId, string name);
        Task<bool> DoesGroupExist(int groupId);
        Task<int> CreateGroup(int workspaceId, string creatorLogin, string name);
        Task<bool> UpdateGroup(int id, string updaterLogin, string name);
        Task<bool> RemoveGroup(int id);
    }

    public class WorkspaceGroupService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IWorkspaceGroupService
    {
        public async Task<bool> DoesGroupExist(int groupId) => await Database.UserGroups.ActiveExists(groupId);
        public async Task<bool> DoesGroupExist(int workspaceId, string name)
        {
            bool found = await Database.UserGroups
                .ActiveWhere(x => x.Name == name && x.Workspace.Id == workspaceId)
                .AnyAsync();

            return found;
        }

        public async Task<int> CreateGroup(int workspaceId, string creatorLogin, string name)
        {
            try
            {
                var newWorkspaceGroup = new DbUserGroup()
                {
                    AddLogin = creatorLogin,
                    Name = name,
                    Workspace = await Database.Workspaces.FindAsync(workspaceId)
                };

                await Database.AddAsync(newWorkspaceGroup);
                await Database.SaveChangesAsync();

                return newWorkspaceGroup.Id;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> UpdateGroup(int id, string updaterLogin, string name)
        {
            try
            {
                if (!await Database.UserGroups.ActiveExists(id))
                    return false;

                Database
                    .UpdateBuilder<DbUserGroup>(id)
                    .Set(x => x.Name, name)
                    .Execute(updaterLogin);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> RemoveGroup(int id)
        {
            try
            {
                if (!await Database.UserGroups.ActiveExists(id))
                    return false;

                Database.Delete<DbUserGroup>(id);
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
