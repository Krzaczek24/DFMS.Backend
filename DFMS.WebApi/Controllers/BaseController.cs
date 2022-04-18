using AutoMapper;
using DFMS.Database;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace DFMS.WebApi.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected AppDbContext Database { get; }
        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }

        public BaseController(ILogger logger, IMapper mapper, AppDbContext database)
        {
            Logger = logger;
            Mapper = mapper;
            Database = database;
        }
    }

    public abstract class BaseController2 : ControllerBase
    {
        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }

        public BaseController2(IMapper mapper)
        {
            Logger = LogManager.GetLogger(GetType().UnderlyingSystemType.FullName);
            Mapper = mapper;
        }
    }
}
