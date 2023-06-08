using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Models;
using Core.Database.Services;
using Core.Database.Tools;
using DFMS.Database.Dto.Permission;
using DFMS.Database.Dto.Users;
using DFMS.Database.Exceptions;
using DFMS.Database.Models;
using KrzaqTools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IPermissionService
    {
        #region Permission
        public Task<int> CreatePermission(string creatorLogin, string name, string description, bool? active = null);
        public Task<bool> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null);
        public Task<bool> RemovePermission(int id);
        #endregion

        #region Permission group
        public Task<int> CreatePermissionGroup(string creatorLogin, string name, string description, bool? active = null);
        public Task<bool> UpdatePermissionGroup(int id, string updaterLogin, string name = null, string description = null, bool? active = null);
        public Task<bool> RemovePermissionGroup(int id);
        #endregion

        #region Permission to group assignment
        public Task AddPermissionToGroup(string creatorLogin, int permissionGroupId, int permissionId);
        public Task<bool> RemovePermissionFromGroup(int permissionGroupId, int permissionId);
        #endregion

        #region User to group assignment
        public Task AssignUserToPermissionGroup(string creatorLogin, int permissionGroupId, int userId, DateTime? validUntil = null);
        public Task<bool> UpdateUserPermissionGroupAssignment(string updaterLogin, int permissionGroupId, int userId, Specifiable<DateTime?> validUntil = null);
        public Task<bool> RemoveUserFromPermissionGroup(int permissionGroupId, int userId);
        #endregion

        public Task<PermissionGroup[]> GetPermissionsStructure();
    }

    public class PermissionService : DbService<AppDbContext>, IPermissionService
    {
        public PermissionService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        #region Permission
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorLogin"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
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
        public async Task<bool> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null)
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
        public async Task<bool> RemovePermission(int id)
        {
            try
            {                
                return await Database.Remove<DbUserPermission>(id) > 0;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateExcpetion())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorLogin"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        #region Permission group
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
        public async Task<bool> UpdatePermissionGroup(int id, string updaterLogin, string name = null, string description = null, bool? active = null)
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
        #endregion

        #region Permission to group assignment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorLogin"></param>
        /// <param name="permissionGroupId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task AddPermissionToGroup(string creatorLogin, int permissionGroupId, int permissionId)
        {
            try
            {
                var newAssignment = new DbUserPermissionAssignment()
                {
                    AddLogin = creatorLogin,
                    Permission = await Database.UserPermissions.FindAsync(permissionId),
                    PermissionGroup = await Database.UserPermissionGroups.FindAsync(permissionGroupId)
                };

                await Database.AddAsync(newAssignment);
                await Database.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsDuplicateEntryException())
            {
                throw new DuplicatedEntryException(ex.GetInnerExceptionMessage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        /// <exception cref="CannotDeleteOrUpdateException"></exception>
        public async Task<bool> RemovePermissionFromGroup(int groupId, int permissionId)
        {
            try
            {
                var assignment = await Database.UserPermissionAssignments
                    .Where(x => x.PermissionGroup.Id == groupId && x.Permission.Id == permissionId)
                    .SingleOrDefaultAsync();

                if (assignment == null)
                    return false;

                Database.Remove(assignment);
                await Database.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateExcpetion())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
        #endregion

        #region Permission group to user asignment
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorLogin"></param>
        /// <param name="permissionGroupId"></param>
        /// <param name="userId"></param>
        /// <param name="validUntil"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task AssignUserToPermissionGroup(string creatorLogin, int permissionGroupId, int userId, DateTime? validUntil = null)
        {
            try
            {
                var newAssignment = new DbUserPermissionGroupAssignment()
                {
                    AddLogin = creatorLogin,
                    User = await Database.Users.FindAsync(userId),
                    PermissionGroup = await Database.UserPermissionGroups.FindAsync(permissionGroupId),
                    ValidUntil = validUntil
                };

                await Database.AddAsync(newAssignment);
                await Database.SaveChangesAsync();
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
        /// <param name="validUntil"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<bool> UpdateUserPermissionGroupAssignment(string updaterLogin, int permissionGroupId, int userId, Specifiable<DateTime?> validUntil)
        {
            try
            {
                var assignment = await Database.UserPermissionGroupAssignments
                    .Where(x => x.PermissionGroup.Id == permissionGroupId && x.User.Id == userId)
                    .SingleOrDefaultAsync();

                if (assignment == null)
                    return false;

                return await Database
                    .Update<DbUserPermissionGroupAssignment>(assignment.Id)
                    .Set(x => x.ValidUntil, validUntil)
                    .Execute(updaterLogin) > 0;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateExcpetion())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CannotDeleteOrUpdateException"></exception>
        public async Task<bool> RemoveUserFromPermissionGroup(int permissionGroupId, int userId)
        {
            try
            {
                var assignment = await Database.UserPermissionGroupAssignments
                    .Where(x => x.PermissionGroup.Id == permissionGroupId && x.User.Id == userId)
                    .SingleOrDefaultAsync();

                if (assignment == null)
                    return false;

                Database.Remove(assignment);
                await Database.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateExcpetion())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
        #endregion

        public async Task<PermissionGroup[]> GetPermissionsStructure()
        {
            var query = from upg in Database.UserPermissionGroups
                        select new PermissionGroup()
                        {
                            Id = upg.Id,
                            Name = upg.Name,
                            Description = upg.Description,
                            Active = upg.Active.Value,
                            Permissions = (from upa in Database.UserPermissionAssignments
                                           join up in Database.UserPermissions on upa.Permission.Id equals up.Id
                                           where upa.PermissionGroup.Id == upg.Id
                                           select new Permission()
                                           { 
                                               Id = up.Id,
                                               Name = up.Name,
                                               Description= up.Description,
                                               Active = up.Active.Value
                                           }).AsEnumerable().ToArray()
                        };

            return await query.ToArrayAsync();
        }
    }
}
