using AutoMapper;
using DFMS.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFMS.WebApi.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase where T : ControllerBase
    {
        protected AppDbContext Database { get; }
        protected ILogger<T> Logger { get; }
        protected IMapper Mapper { get; }

        //- TO REMOVE -
        protected const string UserLogin = "TEMP";
        //-------------

        public BaseController(ILogger<T> logger, IMapper mapper, AppDbContext database)
        {
            Logger = logger;
            Mapper = mapper;
            Database = database;
        }
    }
}
