using AutoMapper;
using DFMS.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using KrzaqTools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Core.Database.Extensions;

#nullable enable
namespace DFMS.Database.Services.Permissions
{
    public interface IUserToPermissionGroupAssigmentService
    {
        Task AssignUserToPermissionGroup(string creatorLogin, int permissionGroupId, int userId, DateTime? validUntil = null);
        Task<bool> UpdateUserPermissionGroupAssignment(string updaterLogin, int permissionGroupId, int userId, Specifiable<DateTime?> validUntil);
        Task<bool> RemoveUserFromPermissionGroup(int permissionGroupId, int userId);
    }

    public class UserToPermissionGroupAssigmentService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IUserToPermissionGroupAssigmentService
    {
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

        public async Task<bool> UpdateUserPermissionGroupAssignment(string updaterLogin, int permissionGroupId, int userId, Specifiable<DateTime?> validUntil)
        {
            try
            {
                var assignment = await Database.UserPermissionGroupAssignments
                    .ActiveWhere(x => x.PermissionGroup.Id == permissionGroupId && x.User.Id == userId)
                    .SingleOrDefaultAsync();

                if (assignment == null)
                    return false;

                Database
                    .UpdateBuilder<DbUserPermissionGroupAssignment>(assignment.Id)
                    .Set(x => x.ValidUntil, validUntil)
                    .Execute(updaterLogin);
                await Database.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex) when (ex.IsCannotDeleteOrUpdateException())
            {
                throw new CannotDeleteOrUpdateException(ex.GetInnerExceptionMessage());
            }
        }

        public async Task<bool> RemoveUserFromPermissionGroup(int permissionGroupId, int userId)
        {
            try
            {
                var assignment = await Database.UserPermissionGroupAssignments
                    .ActiveWhere(x => x.PermissionGroup.Id == permissionGroupId && x.User.Id == userId)
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

