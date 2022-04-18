using AutoMapper;
using DFMS.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : BaseController
    {
        public AuthorizationController(ILogger logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }


    }
}