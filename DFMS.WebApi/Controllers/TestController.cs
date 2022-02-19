using AutoMapper;
using DFMS.Database;
using DFMS.Shared.Tools;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public TestController(ILogger<TestController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        [HttpGet("CheckApiConnection")]
        public IActionResult CheckApiConnection()
        {
            return Ok(new { message = "Test połączenia zakończony pomyślnie" });
        }

        [HttpGet("CheckDbConnection")]
        public IActionResult CheckDbConnection()
        {
            var entities = Database.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType).Select(p => p.Name).ToList();
            var result = new List<Entity>();

            foreach (var entity in entities)
            {
                try
                {
                    var records = ReflectionToolbox.GetObjectPropertyValue<IEnumerable<object>>(Database, entity);
                    result.Add(new Entity () { Name = entity, Rows = records.Count(), Success = true });
                }
                catch (Exception ex)
                {
                    result.Add(new Entity() { Name = entity, ErrorMessage = ex.Message });                    
                    Logger.LogError(ex, $"Wystąpił błąd podczas próby sprawdzenia tabeli [{entity}]");
                }
            }

            var badEntities = result.Where(x => !x.Success);
            bool success = badEntities.Count() == 0;
            badEntities = success ? null : badEntities;

            return new JsonResult(new {
                entitiesCount = result.Count,
                recordsCount = result.Sum(x => x.Rows),
                success,
                badEntities,
                entities = result
            });
        }

        private class Entity
        {
            public string Name { get; set; }
            public int? Rows { get; set; }
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
#endif
}
