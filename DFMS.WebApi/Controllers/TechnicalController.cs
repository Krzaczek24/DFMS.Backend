﻿using AutoMapper;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route(ControllerGroup.Api + "/technical")]
    public class TechnicalController : ResponseController
    {
        public TechnicalController(IMapper mapper) : base(mapper) { }

        [HttpPost("ping")]
        public void Ping() {  }
    }
}
