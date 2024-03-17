using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Dto.Workspace;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Workspace
{
    public interface IWorkspaceService
    {
        Task<bool> DoesWorkspaceExist(string name);
        Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic);
        Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null, bool? isActive = null);
        Task<WorkspaceDto[]> GetWorkspacesList();
        Task<WorkspaceGroupDto[]> GetWorkspaceStructure(int id);
    }

    public class WorkspaceService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IWorkspaceService
    {
        public async Task<bool> DoesWorkspaceExist(string name)
        {
            bool found = await Database.Workspaces
                .ActiveWhere(x => x.Name == name)
                .AnyAsync();

            return found;
        }

        public async Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic)
        {
            try
            {
                var newPermission = new DbWorkspace()
                {
                    AddLogin = creatorLogin,
                    Name = name,
                    Public = isPublic                    
                };

                await Database.AddAsync(newPermission);
                await Database.SaveChangesAsync();

                return newPermission.Id;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null, bool? isActive = null)
        {
            try
            {
                if (!await Database.Workspaces.ActiveExists(id))
                    return false;

                Database
                    .UpdateBuilder<DbWorkspace>(id)
                    .SetIfNotNullOrDefault(x => x.Name, name)
                    .SetIfNotNullOrDefault(x => x.Public, isPublic)
                    .SetIfNotNullOrDefault(x => x.Active, isActive)
                    .Execute(updaterLogin);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<WorkspaceDto[]> GetWorkspacesList()
        {
            var query = from w in Database.Workspaces
                        select new WorkspaceDto()
                        {
                            Id = w.Id,
                            Name = w.Name,
                        };

            return await query.ToArrayAsync();
        }

        public async Task<WorkspaceGroupDto[]> GetWorkspaceStructure(int id)
        {
            var query = from ug in Database.UserGroups
                        where ug.Workspace.Id == id
                        select new WorkspaceGroupDto()
                        {
                            Id = ug.Id,
                            Name = ug.Name,
                            Members = (from ugm in Database.UserGroupMembers
                                       join u in Database.Users on ugm.User.Id equals u.Id
                                       where ugm.Group.Id == ug.Id
                                       select new WorkspaceGroupMemberDto()
                                       {
                                           Id = ugm.Id,
                                           Login = u.Login
                                       }).AsEnumerable().ToArray()
                        };

            return await query.ToArrayAsync();
        }
    }
}
