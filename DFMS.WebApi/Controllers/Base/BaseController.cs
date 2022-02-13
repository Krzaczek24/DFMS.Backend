using DFMS.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFMS.WebApi.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase where T : ControllerBase
    {
        protected AppDbContext Database { get; }
        protected ILogger<T> Logger { get; }

        public BaseController(ILogger<T> logger, AppDbContext database)
        {
            Logger = logger;
            Database = database;
        }
    }
}
