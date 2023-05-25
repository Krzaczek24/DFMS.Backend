using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate)]
    public class FormTemplateController : BaseController
    {
        public FormTemplateController(IMapper mapper) : base(mapper) { }
    }
}
