using AutoMapper;
using DFMS.Database.Dto.Permission;
using DFMS.Database.Exceptions;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Database.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DFMS.Database.Services
{
    public interface IPermissionService
    {
        #region Permission
        public Task<int> AddPermission(string creatorLogin, string name, string description, bool? active = null);
        public Task<int> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null);
        public Task<int> RemovePermission(int id);
        #endregion

        #region Permission group
        public Task<int> AddPermissionGroup(string creatorLogin, string name, string description, bool? active = null);
        public Task<int> UpdatePermissionGroup(int id, string updaterLogin, string name = null, string description = null, bool? active = null);
        public Task<int> RemovePermissionGroup(int id);
        #endregion

        #region Permission to group assignment
        public Task<int> AssignPermissionToGroup(string creatorLogin, int permissionId, int permissionGroupId, bool? active = null);
        public Task<int> UpdateGroupPermissionAssignment(int id, string updaterLogin, bool? active = null);
        public Task<int> UnassignPermissionFromGroup(int id);
        #endregion

        #region Permission group to user asignment
        public Task<int> AssignPermissionGroupToUser(string creatorLogin, int userId, int permissionGroupId, DateTime? validUntil = null, bool? active = null);
        public Task<int> UpdateUserPermissionGroupAssignment(int id, string updaterLogin, Specifiable<DateTime?> validUntil = null, bool? active = null);
        public Task<int> UnassignPermissionGroupFromUser(int id);
        #endregion

        public Task<PermissionGroup[]> GetPermissionsStructure();
    }

    public class PermissionService : DbService, IPermissionService
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
        public async Task<int> AddPermission(string creatorLogin, string name, string description, bool? active = null)
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
        public async Task<int> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null)
        {
            try
            {
                var result = await Database
                    .Update<DbUserPermission>(id)
                    .SetIfNotNullOrDefault(x => x.Name, name)
                    .SetIfNotNullOrDefault(x => x.Description, description)
                    .SetIfNotNullOrDefault(x => x.Active, active)
                    .Execute(updaterLogin);
                return result;
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
        public async Task<int> RemovePermission(int id)
        {
            try
            {
                var result = await Database.Remove<DbUserPermission>(id);
                return result;
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
        public async Task<int> AddPermissionGroup(string creatorLogin, string name, string description, bool? active = null)
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
        public async Task<int> UpdatePermissionGroup(int id, string updaterLogin, string name = null, string description = null, bool? active = null)
        {
            try
            {
                var result = await Database
                .Update<DbUserPermission>(id)
                .SetIfNotNullOrDefault(x => x.Name, name)
                .SetIfNotNullOrDefault(x => x.Description, description)
                .SetIfNotNullOrDefault(x => x.Active, active)
                .Execute(updaterLogin);
                return result;
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
        public async Task<int> RemovePermissionGroup(int id)
        {
            try
            {
                var result = await Database.Remove<DbUserPermissionGroup>(id);
                return result;
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
        /// <param name="permissionId"></param>
        /// <param name="permissionGroupId"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<int> AssignPermissionToGroup(string creatorLogin, int permissionId, int permissionGroupId, bool? active = null)
        {
            try
            {
                var newAssignment = new DbUserPermissionAssignment()
                {
                    AddLogin = creatorLogin,
                    Permission = await Database.UserPermissions.FindAsync(permissionId),
                    PermissionGroup = await Database.UserPermissionGroups.FindAsync(permissionGroupId),
                    Active = active
                };

                await Database.AddAsync(newAssignment);
                await Database.SaveChangesAsync();

                return newAssignment.Id;
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
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<int> UpdateGroupPermissionAssignment(int id, string updaterLogin, bool? active = null)
        {
            try
            {
                var result = await Database
                    .Update<DbUserPermissionAssignment>(id)
                    .SetIfNotNullOrDefault(x => x.Active, active)
                    .Execute(updaterLogin);
                return result;
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
        public async Task<int> UnassignPermissionFromGroup(int id)
        {
            try
            {
                var result = await Database.Remove<DbUserPermissionAssignment>(id);
                return result;
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
        /// <param name="userId"></param>
        /// <param name="permissionGroupId"></param>
        /// <param name="validUntil"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<int> AssignPermissionGroupToUser(string creatorLogin, int userId, int permissionGroupId, DateTime? validUntil = null, bool? active = null)
        {
            try
            {
                var newAssignment = new DbUserPermissionGroupAssignment()
                {
                    AddLogin = creatorLogin,
                    User = await Database.Users.FindAsync(userId),
                    PermissionGroup = await Database.UserPermissionGroups.FindAsync(permissionGroupId),
                    ValidUntil = validUntil,
                    Active = active
                };

                await Database.AddAsync(newAssignment);
                await Database.SaveChangesAsync();

                return newAssignment.Id;
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
        /// <param name="active"></param>
        /// <returns></returns>
        /// <exception cref="DuplicatedEntryException"></exception>
        public async Task<int> UpdateUserPermissionGroupAssignment(int id, string updaterLogin, Specifiable<DateTime?> validUntil = null, bool? active = null)
        {
            try
            {
                var result = await Database
                    .Update<DbUserPermissionGroupAssignment>(id)
                    .Set(x => x.ValidUntil, validUntil)
                    .SetIfNotNullOrDefault(x => x.Active, active)
                    .Execute(updaterLogin);
                return result;
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
        public async Task<int> UnassignPermissionGroupFromUser(int id)
        {
            try
            {
                var result = await Database.Remove<DbUserPermissionGroupAssignment>(id);
                return result;
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
