using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorLogin"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updaterLogin"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<bool> UpdatePermissionGroup(int id, string updaterLogin, string? name = null, string? description = null, bool? active = null)
        {
            try
            {
                return await Database
                    .Update<DbUserPermission>(id)
                    .SetIfNotNullOrDefault(x => x.Name, name)
                    .SetIfNotNullOrDefault(x => x.Description, description)
                    .SetIfNotNullOrDefault(x => x.Active, active)
                    .Execute(updaterLogin) > 0;
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CannotDeleteOrUpdateException"></exception>
        public async Task<bool> RemovePermissionGroup(int id)
        {
            try
            {
                return await Database.Remove<DbUserPermissionGroup>(id) > 0;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateExcpetion())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
    }
}
