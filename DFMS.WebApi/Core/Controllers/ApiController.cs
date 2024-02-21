using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.WebApi.Core.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Core.Controllers
{
    [ApiExplorerSettings(GroupName = ControllerGroup.Api)]
    public class ApiController(IMapper mapper) : BaseController(mapper)
    {
        
    }
}
