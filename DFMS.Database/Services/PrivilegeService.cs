using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.Database.Services
{
    public interface IPrivilegeService
    {
        #region Privilege
        public Task AddPrivilege(string name, string description);
        public Task RemovePrivilege(string name);
        public Task UpdatePrivilege(string name, string newName = null, string description = null, bool? isActive = null);
        #endregion

        #region Privilege group
        public Task AddPrivilegeGroup(string name, string description);
        public Task RemovePrivilegeGroup(string name);
        public Task UpdatePrivilegeGroup(string name, string newName = null, string description = null, bool? isActive = null);
        #endregion

        #region Privilege to group assignment
        public Task AssignPrivilegeToGroup(string privilege, string group);
        public Task UnassignPrivilegeFromGroup(string privilege, string group);
        public Task UpdateGroupPrivilegeAssignment(string privilege, string group, bool isActive);
        #endregion

        #region Privilege group to user asignment
        public Task AssignPrivilegeGroupToUser(string userLogin, string privilegeGroup, DateTime? validUntil = null);
        public Task UnassignPrivilegeGroupFromUser(string userLogin, string privilegeGroup);
        public Task UpdateUserPrivilegeGroupAssignment(string userLogin, string privilegeGroup, DateTime? validUntil);
        #endregion

        public Task<IEnumerable<string>> GetPrivilegesStructure();
    }

    public class PrivilegeService : DbService, IPrivilegeService
    {
        public PrivilegeService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        #region Privilege
        public async Task AddPrivilege(string name, string description)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePrivilege(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePrivilege(string name, string newName = null, string description = null, bool? isActive = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Privilege group
        public async Task AddPrivilegeGroup(string name, string description)
        {
            throw new NotImplementedException();
        }

        public async Task RemovePrivilegeGroup(string name)
        {
            throw new NotImplementedException();
        }

        public async Task UpdatePrivilegeGroup(string name, string newName = null, string description = null, bool? isActive = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Privilege to group assignment
        public async Task AssignPrivilegeToGroup(string privilege, string group)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPrivilegeFromGroup(string privilege, string group)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGroupPrivilegeAssignment(string privilege, string group, bool isActive)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Privilege group to user asignment
        public async Task AssignPrivilegeGroupToUser(string userLogin, string privilegeGroup, DateTime? validUntil = null)
        {
            throw new NotImplementedException();
        }

        public async Task UnassignPrivilegeGroupFromUser(string userLogin, string privilegeGroup)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPrivilegeGroupAssignment(string userLogin, string privilegeGroup, DateTime? validUntil)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<IEnumerable<string>> GetPrivilegesStructure()
        {
            throw new NotImplementedException();
        }
    }
}
