using AutoMapper;
using DFMS.Database;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route("form")]
    public class FormController : BaseController<FormController>
    {
        public FormController(ILogger<FormController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        
    }
}