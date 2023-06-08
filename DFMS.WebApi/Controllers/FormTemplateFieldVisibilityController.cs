﻿using AutoMapper;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate + "/visibility-rules")]
    public class FormTemplateFieldVisibilityController : ResponseController
    {
        public FormTemplateFieldVisibilityController(IMapper mapper) : base(mapper) { }
    }
}
