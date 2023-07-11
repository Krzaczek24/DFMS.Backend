using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Core.Controllers
{
    [ApiExplorerSettings(GroupName = ControllerGroup.Api)]
    public class ApiController : BaseController
    {
        public ApiController(IMapper mapper) : base(mapper) { }
    }
}
