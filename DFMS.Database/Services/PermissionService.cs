using AutoMapper;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IPermissionService
    {
        #region Permission
        public Task<int> AddPermission(string creatorLogin, string name, string description);
        public Task<int> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null);
        public Task<int> RemovePermission(int id);
        #endregion

        #region Permission group
        public Task AddPermissionGroup(string creatorLogin, string name, string description);
        public Task UpdatePermissionGroup(string updaterLogin, string name, string newName = null, string description = null, bool? isActive = null);
        public Task RemovePermissionGroup(string name);
        #endregion

        #region Permission to group assignment
        public Task AssignPermissionToGroup(string creatorLogin, string permission, string group);
        public Task UpdateGroupPermissionAssignment(string updaterLogin, string permission, string group, bool isActive);
        public Task UnassignPermissionFromGroup(string permission, string group);
        #endregion

        #region Permission group to user asignment
        public Task AssignPermissionGroupToUser(string creatorLogin, string userLogin, string permissionGroup, DateTime? validUntil = null);
        public Task UpdateUserPermissionGroupAssignment(string updaterLogin, string userLogin, string permissionGroup, DateTime? validUntil);
        public Task UnassignPermissionGroupFromUser(string userLogin, string permissionGroup);
        #endregion

        public Task<IEnumerable<string>> GetPermissionsStructure();
    }

    public class PermissionService : DbService, IPermissionService
    {
        public PermissionService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        #region Permission
        public async Task<int> AddPermission(string creatorLogin, string name, string description)
        {
            var newPermission = new DbUserPermission()
            {
                AddLogin = creatorLogin,
                Name = name,
                Description = description
            };

            await Database.AddAsync(newPermission);
            await Database.SaveChangesAsync();

            return newPermission.Id;
        }

        public async Task<int> RemovePermission(int id)
        {
            var result = await Database.Remove<DbUserPermission>(id);
            return result;
        }

        public async Task<int> UpdatePermission(int id, string updaterLogin, string name = null, string description = null, bool? active = null)
        {
            var result = await Database
                .Update<DbUserPermission>(id)
                .SetIfNotNullOrDefault(x => x.Name, name)
                .SetIfNotNullOrDefault(x => x.Description, description)
                .SetIfNotNullOrDefault(x => x.Active, active)
                .Execute(updaterLogin);
            return result;
        }
        #endregion

        #region Permission group
        public async Task AddPermissionGroup(string creatorLogin, string name, string description)
        {
            throw new NotImplementedException();
        }        

        public async Task UpdatePermissionGroup(string updaterLogin, string name, string newName = null, string description = null, bool? isActive = null)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePermissionGroup(string name)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission to group assignment
        public async Task AssignPermissionToGroup(string creatorLogin, string Permission, string group)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGroupPermissionAssignment(string updaterLogin, string permission, string group, bool isActive)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPermissionFromGroup(string Permission, string group)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission group to user asignment
        public async Task AssignPermissionGroupToUser(string creatorLogin, string userLogin, string PermissionGroup, DateTime? validUntil = null)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPermissionGroupAssignment(string updaterLogin, string userLogin, string PermissionGroup, DateTime? validUntil)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPermissionGroupFromUser(string userLogin, string PermissionGroup)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<IEnumerable<string>> GetPermissionsStructure()
        {
            throw new NotImplementedException();
        }
    }
}
