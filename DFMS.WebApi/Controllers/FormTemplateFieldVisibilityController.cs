using AutoMapper;
using DFMS.WebApi.Core.Attributes;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("form-template/visibility-rules")]
    public class FormTemplateFieldVisibilityController : ResponseController
    {
        public FormTemplateFieldVisibilityController(IMapper mapper) : base(mapper) { }
    }
}
