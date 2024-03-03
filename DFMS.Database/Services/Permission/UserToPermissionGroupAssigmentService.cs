using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Models;
using DFMS.Shared.Exceptions;
using KrzaqTools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services.Permissions
{
    public interface IUserToGroupAssigmentService
    {
        Task AssignUserToPermissionGroup(string creatorLogin, int permissionGroupId, int userId, DateTime? validUntil = null);
        Task<bool> UpdateUserPermissionGroupAssignment(string updaterLogin, int permissionGroupId, int userId, Specifiable<DateTime?> validUntil);
        Task<bool> RemoveUserFromPermissionGroup(int permissionGroupId, int userId);
    }

    public class UserToPermissionGroupAssigmentService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IUserToGroupAssigmentService
    {
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
    }
}

