﻿using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Permissions
{
    public interface IPermissionToGroupAssigmentService
    {
        Task AddPermissionToGroup(string creatorLogin, int permissionGroupId, int permissionId);
        Task<bool> RemovePermissionFromGroup(int permissionGroupId, int permissionId);
    }

    public class PermissionToGroupAssigmentService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IPermissionToGroupAssigmentService
    {
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
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateException())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }
    }
}
