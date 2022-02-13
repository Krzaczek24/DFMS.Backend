using DFMS.Database;
using DFMS.Shared.Tools;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.WebApi.Controllers
{
#if DEBUG
    [ApiController]
    [Route("[controller]")]
    public class TestController : BaseController<TestController>
    {
        public TestController(ILogger<TestController> logger, AppDbContext database) : base(logger, database)
        {

        }

        [HttpGet("check-api-connection")]
        public IActionResult CheckApiConnection()
        {
            return Ok(new { message = "Test połączenia zakończony pomyślnie" });
        }

        [HttpGet("check-db-connection")]
        public IActionResult CheckDbConnection()
        {
            var result = new List<object>();
            var entities = Database.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType).Select(p => p.Name).ToList();

            foreach (var entity in entities)
            {
                try
                {
                    var records = ReflectionToolbox.GetObjectPropertyValue<IEnumerable<object>>(Database, entity);
                    result.Add(new { entity, success = true, count = records.Count() });
                }
                catch (Exception ex)
                {
                    result.Add(new { entity, success = false, error = ex.Message });
                    Logger.LogError(ex, $"Wystąpił błąd podczas próby sprawdzenia tabeli [{entity}]");
                }
            }

            return new JsonResult(new
            {
                entitiesCount = result.Count,
                success = result.All(x => ((dynamic)x).success),
                entities = result
            });
        }
    }
#endif
}
