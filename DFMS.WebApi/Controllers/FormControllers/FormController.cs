using AutoMapper;
using DFMS.Database;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : BaseController<FormController>
    {
        public FormController(ILogger<FormController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        [HttpGet("check-api-connection")]
        public IActionResult CheckApiConnection()
        {
            return Ok(new { message = "Test połączenia zakończony pomyślnie" });
        }
    }
}