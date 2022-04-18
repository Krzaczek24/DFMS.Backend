using AutoMapper;
using DFMS.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [Authorize]
    [ApiController]
    [Route("form-creator/visibility-rules")]
    public class FormCreatorVisibilityController : BaseController
    {
        public FormCreatorVisibilityController(ILogger logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        
    }
}
