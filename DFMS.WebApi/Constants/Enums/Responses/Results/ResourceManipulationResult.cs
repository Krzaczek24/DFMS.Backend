using DFMS.WebApi.DataContracts;

namespace DFMS.WebApi.Constants.Enums.Responses.Results
{
    public class ResourceManipulationResult<TResource>
    {
        public TResource? Resource { get; set; }
        public ResourceManipulationFailureReason Reason { get; set; }

        public ResourceManipulationResult(TResource resource)
        {
            Resource = resource;
        }

        public ResourceManipulationResult(ResourceManipulationFailureReason reason)
        {
            Reason = reason;
        }
    }

    public static class ResourceManipulationResultExtension
    {
        public static ApiResponse<ResourceManipulationResult<TResult>> WithResourceManipulationResult<TResult>(this ApiResponse response, TResult result)
        {
            return response.WithResult(new ResourceManipulationResult<TResult>(result));
        }

        public static ApiResponse<ResourceManipulationResult<TResult>> WithResourceManipulationResult<TResult>(this ApiResponse response, ResourceManipulationFailureReason reason)
        {
            return response.WithResult(new ResourceManipulationResult<TResult>(reason));
        }
    }
}
