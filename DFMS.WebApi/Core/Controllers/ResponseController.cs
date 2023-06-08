using AutoMapper;
using Core.WebApi.Attributes;
using Core.WebApi.Controllers;
using DFMS.WebApi.Core.Errors;
using System.Net;

namespace DFMS.WebApi.Core.Controllers
{
    [ProducesResponse<ErrorResponse>(HttpStatusCode.BadRequest)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Unauthorized)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Forbidden)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.NotFound)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.Conflict)]
    [ProducesResponse<ErrorResponse>(HttpStatusCode.InternalServerError)]
    public class ResponseController : BaseController
    {
        public ResponseController(IMapper mapper) : base(mapper) { }
    }
}
