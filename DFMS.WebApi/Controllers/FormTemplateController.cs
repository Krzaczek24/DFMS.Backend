﻿using AutoMapper;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate)]
    public class FormTemplateController : ResponseController
    {
        public FormTemplateController(IMapper mapper) : base(mapper) { }
    }
}
