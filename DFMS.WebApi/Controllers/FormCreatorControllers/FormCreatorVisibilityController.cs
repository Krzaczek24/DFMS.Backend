using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [Authorize]
    [ApiController]
    [Route("form-creator/visibility-rules")]
    public class FormCreatorVisibilityController : BaseController
    {
        public FormCreatorVisibilityController(IMapper mapper) : base(mapper) { }

        
    }
}
