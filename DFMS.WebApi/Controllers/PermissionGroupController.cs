using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.Database.Exceptions;
using DFMS.Database.Services;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Constants.Enums.Responses.Results;
using DFMS.WebApi.DataContracts;
using DFMS.WebApi.DataContracts.Permissions;
using DFMS.WebApi.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.PermissionGroup)]
    public class PermissionGroupController : BaseResponseController
    {
        private const string Assignment = "assignment";

        private IPermissionService PermissionService { get; }

        public PermissionGroupController(IMapper mapper, IPermissionService permissionService) : base(mapper)
        {
            PermissionService = permissionService;
        }

        #region Permission group
        [HttpPost]
        public async Task<ApiResponse<ResourceManipulationResult<int>>> AddPermissionGroup([FromBody] AddPermissionGroupInput input)
        {
            return await Executor.ExecuteAndHandleResourceCreation(async () => await PermissionService.AddPermissionGroup(User.GetLogin(), input.Name, input.Description, input.Active));

            try
            {
                var result = PermissionService.AddPermissionGroup(User.GetLogin(), input.Name, input.Description, input.Active);
                return ApiResponse.Success.WithResult(new ResourceManipulationResult<int>(await result));
            }
            catch (DuplicatedEntryException)
            {
                return ApiResponse.Failure.WithResult(new ResourceManipulationResult<int>(ResourceManipulationFailureReason.NotUnique));
            }
        }

        [HttpPatch("{id}")]
        public async Task<ApiResponse<ResourceManipulationFailureReason>> UpdatePermissionGroup([FromRoute] int id, [FromBody] UpdatePermissionGroupInput input)
        {
            return await Executor.ExecuteAndHandleResourceUpdate(async () => await PermissionService.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active));

            try
            {
                return await PermissionService.UpdatePermissionGroup(id, User.GetLogin(), input.Name, input.Description, input.Active) ?
                    ApiResponse.Success.As<ResourceManipulationFailureReason>() :
                    ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotFound);
            }
            catch (DuplicatedEntryException)
            {
                return ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotUnique);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse<ResourceManipulationFailureReason>> RemovePermissionGroup([FromRoute] int id)
        {
            return await Executor.ExecuteAndHandleResourceRemoval(async () => await PermissionService.RemovePermissionGroup(id));

            try
            {
                return await PermissionService.RemovePermissionGroup(id) ? 
                    ApiResponse.Success.As<ResourceManipulationFailureReason>() : 
                    ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                return ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.InUse);
            }
        }
        #endregion

        #region Permission group to user assignment
        [HttpPost(Assignment)]
        public async Task<ApiResponse<ResourceManipulationResult<int>>> AssignPermissionGroupToUser([FromBody] AssignPermissionGroupToUserInput input)
        {
            return await Executor.ExecuteAndHandleResourceCreation(async () => await PermissionService.AssignPermissionGroupToUser(User.GetLogin(), input.PermissionId, input.PermissionGroupId, input.ValidUntil));

            try
            {
                var result = PermissionService.AssignPermissionGroupToUser(User.GetLogin(), input.PermissionId, input.PermissionGroupId, input.ValidUntil);
                return ApiResponse.Success.WithResult(new ResourceManipulationResult<int>(await result));
            }
            catch (DuplicatedEntryException)
            {
                return ApiResponse.Failure.WithResult(new ResourceManipulationResult<int>(ResourceManipulationFailureReason.NotUnique));
            }
        }

        [HttpPatch(Assignment + "/{id}")]
        public async Task<ApiResponse<ResourceManipulationFailureReason>> UpdateUserPermissionGroupAssignment([FromRoute] int id, [FromBody] UpdatePermissionGroupToUserAssignmentInput input)
        {
            return await Executor.ExecuteAndHandleResourceUpdate(async () => await PermissionService.UpdateUserPermissionGroupAssignment(id, User.GetLogin(), input.ValidUntil));

            try
            {
                return await PermissionService.UpdateUserPermissionGroupAssignment(id, User.GetLogin(), input.ValidUntil) ?
                    ApiResponse.Success.As<ResourceManipulationFailureReason>() :
                    ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                return ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotUnique);
            }
        }

        [HttpDelete(Assignment + "/{id}")]
        public async Task<ApiResponse<ResourceManipulationFailureReason>> UnassignPermissionGroupFromUser([FromRoute] int id)
        {
            return await Executor.ExecuteAndHandleResourceRemoval(async () => await PermissionService.UnassignPermissionGroupFromUser(id));

            try
            {
                return await PermissionService.UnassignPermissionGroupFromUser(id) ?
                    ApiResponse.Success.As<ResourceManipulationFailureReason>() :
                    ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.NotFound);
            }
            catch (CannotDeleteOrUpdateException)
            {
                return ApiResponse.Failure.WithResult(ResourceManipulationFailureReason.InUse);
            }
        }
        #endregion
    }
}
