using AutoMapper;
using Core.WebApi.Attributes;
using DFMS.WebApi.Core.Errors;
using System.Net;

namespace DFMS.WebApi.Core.Controllers
{
    [ProducesResponse(HttpStatusCode.OK)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.BadRequest)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Unauthorized)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Forbidden)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.NotFound)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Conflict)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.InternalServerError)]
    public class ResponseController(IMapper mapper) : ApiController(mapper)
    {
        
    }
}
