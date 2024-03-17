using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Dto.Permissions;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Permissions
{
    public interface IPermissionService
    {
        Task<int> CreatePermission(string creatorLogin, string name, string description, bool? active = null);
        Task<bool> UpdatePermission(int id, string updaterLogin, string? name = null, string? description = null, bool? active = null);
        Task<bool> RemovePermission(int id);
        Task<PermissionGroupDto[]> GetPermissionsStructure();
    }

    public class PermissionService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IPermissionService
    {
        public async Task<int> CreatePermission(string creatorLogin, string name, string description, bool? active = null)
        {
            try
            {
                var newPermission = new DbUserPermission()
                {
                    AddLogin = creatorLogin,
                    Name = name,
                    Description = description,
                    Active = active
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

        public async Task<bool> UpdatePermission(int id, string updaterLogin, string? name = null, string? description = null, bool? active = null)
        {
            try
            {
                if (!await Database.UserPermissions.ActiveExists(id))
                    return false;

                Database
                    .UpdateBuilder<DbUserPermission>(id)
                    .SetIfNotNullOrDefault(x => x.Name, name)
                    .SetIfNotNullOrDefault(x => x.Description, description)
                    .SetIfNotNullOrDefault(x => x.Active, active)
                    .Execute(updaterLogin);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> RemovePermission(int id)
        {
            try
            {
                if (!await Database.UserPermissions.ActiveExists(id))
                    return false;

                Database.Delete<DbUserPermission>(id);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateException())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<PermissionGroupDto[]> GetPermissionsStructure()
        {
            var query = from upg in Database.UserPermissionGroups
                        select new PermissionGroupDto()
                        {
                            Id = upg.Id,
                            Name = upg.Name,
                            Description = upg.Description,
                            Active = upg.Active!.Value,
                            Permissions = (from upa in Database.UserPermissionAssignments
                                           join up in Database.UserPermissions on upa.Permission.Id equals up.Id
                                           where upa.PermissionGroup.Id == upg.Id
                                           select new PermissionDto()
                                           {
                                               Id = up.Id,
                                               Name = up.Name,
                                               Description = up.Description,
                                               Active = up.Active!.Value
                                           }).AsEnumerable().ToArray()
                        };

            return await query.ToArrayAsync();
        }
    }
}
