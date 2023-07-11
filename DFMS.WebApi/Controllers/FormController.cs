using AutoMapper;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(ControllerGroup.Api + "/form")]
    public class FormController : ResponseController
    {
        public FormController(IMapper mapper) : base(mapper) { }
    }
}