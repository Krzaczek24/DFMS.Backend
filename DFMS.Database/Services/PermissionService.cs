using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IPermissionService
    {
        #region Permission
        public Task AddPermission(string name, string description);
        public Task RemovePermission(string name);
        public Task UpdatePermission(string name, string newName = null, string description = null, bool? isActive = null);
        #endregion

        #region Permission group
        public Task AddPermissionGroup(string name, string description);
        public Task RemovePermissionGroup(string name);
        public Task UpdatePermissionGroup(string name, string newName = null, string description = null, bool? isActive = null);
        #endregion

        #region Permission to group assignment
        public Task AssignPermissionToGroup(string Permission, string group);
        public Task UnassignPermissionFromGroup(string Permission, string group);
        public Task UpdateGroupPermissionAssignment(string Permission, string group, bool isActive);
        #endregion

        #region Permission group to user asignment
        public Task AssignPermissionGroupToUser(string userLogin, string PermissionGroup, DateTime? validUntil = null);
        public Task UnassignPermissionGroupFromUser(string userLogin, string PermissionGroup);
        public Task UpdateUserPermissionGroupAssignment(string userLogin, string PermissionGroup, DateTime? validUntil);
        #endregion

        public Task<IEnumerable<string>> GetPermissionsStructure();
    }

    public class PermissionService : DbService, IPermissionService
    {
        public PermissionService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        #region Permission
        public async Task AddPermission(string name, string description)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePermission(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePermission(string name, string newName = null, string description = null, bool? isActive = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission group
        public async Task AddPermissionGroup(string name, string description)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePermissionGroup(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePermissionGroup(string name, string newName = null, string description = null, bool? isActive = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission to group assignment
        public async Task AssignPermissionToGroup(string Permission, string group)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPermissionFromGroup(string Permission, string group)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGroupPermissionAssignment(string Permission, string group, bool isActive)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Permission group to user asignment
        public async Task AssignPermissionGroupToUser(string userLogin, string PermissionGroup, DateTime? validUntil = null)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPermissionGroupFromUser(string userLogin, string PermissionGroup)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPermissionGroupAssignment(string userLogin, string PermissionGroup, DateTime? validUntil)
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
