using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Permissions
{
    public interface IPermissionGroupService
    {
        Task<int> CreatePermissionGroup(string creatorLogin, string name, string description, bool? active = null);
        Task<bool> UpdatePermissionGroup(int id, string updaterLogin, string? name = null, string? description = null, bool? active = null);
        Task<bool> RemovePermissionGroup(int id);
    }

    public class PermissionGroupService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IPermissionGroupService
    {
        public async Task<int> CreatePermissionGroup(string creatorLogin, string name, string description, bool? active = null)
        {
            try
            {
                var newPermissionGroup = new DbUserPermissionGroup()
                {
                    AddLogin = creatorLogin,
                    Name = name,
                    Description = description,
                    Active = active
                };

                await Database.AddAsync(newPermissionGroup);
                await Database.SaveChangesAsync();

                return newPermissionGroup.Id;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> UpdatePermissionGroup(int id, string updaterLogin, string? name = null, string? description = null, bool? active = null)
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

        public async Task<bool> RemovePermissionGroup(int id)
        {
            try
            {
                if (!await Database.UserPermissionGroups.ActiveExists(id))
                    return false;

                Database.Delete<DbUserPermissionGroup>(id);
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
