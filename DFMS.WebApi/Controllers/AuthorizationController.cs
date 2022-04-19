using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(IMapper mapper) : base(mapper) { }


    }
}