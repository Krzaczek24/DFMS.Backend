using DFMS.Database.Exceptions;
using DFMS.WebApi.Constants.Enums.Responses.Results;
using DFMS.WebApi.DataContracts;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace DFMS.WebApi.Tools
{
    public static class Executor
    {
        public static async Task<ApiResponse<TFailureResult>> ExecuteAndHandle<TFailureResult>(
            Func<Task<bool>> function,
            TFailureResult onNonSuccessful,
            Dictionary<Type, TFailureResult> onException)
        {
            try
            {
                return await function() ?
                    ApiResponse.Success.As<TFailureResult>() :
                    ApiResponse.Failure.WithResult(onNonSuccessful);
            }
            catch (Exception ex) when (onException.TryGetValue(ex.GetType(), out TFailureResult? result))
            {
                return ApiResponse.Failure.WithResult(result);
            }
        }

        public static async Task<ApiResponse<ResourceManipulationResult<TResult>>> ExecuteAndHandleResourceCreation<TResult>(
            Func<Task<TResult>> function,
            Dictionary<Type, ResourceManipulationFailureReason> onException)
        {
            try
            {
                return ApiResponse.Success.WithResourceManipulationResult(await function());
            }
            catch (Exception ex) when (onException.TryGetValue(ex.GetType(), out ResourceManipulationFailureReason reason))
            {
                return ApiResponse.Failure.WithResourceManipulationResult<TResult>(reason);
            }
        }

        public static async Task<ApiResponse<ResourceManipulationResult<TResult>>> ExecuteAndHandleResourceCreation<TResult>(Func<Task<TResult>> function)
        {
            return await ExecuteAndHandleResourceCreation(function, ResourceAdditionExceptionReasons);
        }

        public static async Task<ApiResponse<ResourceManipulationFailureReason>> ExecuteAndHandleResourceUpdate(Func<Task<bool>> function)
        {
            return await ExecuteAndHandle(function, ResourceManipulationFailureReason.NotFound, ResourceUpdateExceptionReasons);
        }

        public static async Task<ApiResponse<ResourceManipulationFailureReason>> ExecuteAndHandleResourceRemoval(Func<Task<bool>> function)
        {
            return await ExecuteAndHandle(function, ResourceManipulationFailureReason.NotFound, ResourceRemovalExceptionReasons);
        }

        #region STATIC DICTIONARIES
        private static Dictionary<Type, ResourceManipulationFailureReason> ResourceAdditionExceptionReasons { get; } = new Dictionary<Type, ResourceManipulationFailureReason>()
        {
            [typeof(DuplicatedEntryException)] = ResourceManipulationFailureReason.NotUnique
        };

        private static Dictionary<Type, ResourceManipulationFailureReason> ResourceUpdateExceptionReasons { get; } = new Dictionary<Type, ResourceManipulationFailureReason>()
        {
            [typeof(CannotDeleteOrUpdateException)] = ResourceManipulationFailureReason.NotUnique
        };

        private static Dictionary<Type, ResourceManipulationFailureReason> ResourceRemovalExceptionReasons { get; } = new Dictionary<Type, ResourceManipulationFailureReason>()
        {
            [typeof(DuplicatedEntryException)] = ResourceManipulationFailureReason.InUse
        };
        #endregion STATIC DICTIONARIES
    }
}
