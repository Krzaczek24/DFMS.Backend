using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace DFMS.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class TechnicalController
    {
        public TechnicalController() { }

        [HttpPost("/ping")]
        public void Ping() { Thread.Sleep(100); }
    }
}
