using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.WebApi.Common.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Common.Controllers
{
    [ApiExplorerSettings(GroupName = ControllerGroup.Api)]
    public abstract class ApiController(IMapper mapper) : BaseController(mapper) { }
}
