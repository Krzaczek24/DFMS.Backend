using AutoMapper;
using DFMS.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("form")]
    public class FormController : BaseController
    {
        public FormController(ILogger logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        
    }
}