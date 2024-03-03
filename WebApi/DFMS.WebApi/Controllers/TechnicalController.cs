using AutoMapper;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiRoute("technical")]
    public class TechnicalController(IMapper mapper) : ResponseController(mapper)
    {
        [HttpPost("ping")]
        public void Ping() { }
    }
}
