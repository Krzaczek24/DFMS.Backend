using AutoMapper;
using Core.WebApi.Attributes;
using DFMS.WebApi.Common.Errors;
using System.Net;

namespace DFMS.WebApi.Common.Controllers
{
    [ProducesResponse(HttpStatusCode.OK)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.BadRequest)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Unauthorized)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Forbidden)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.NotFound)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Conflict)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.InternalServerError)]
    public abstract class ResponseController(IMapper mapper) : ApiController(mapper) { }
}
