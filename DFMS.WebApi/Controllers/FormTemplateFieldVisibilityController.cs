﻿using AutoMapper;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate + "/visibility-rules")]
    public class FormTemplateFieldVisibilityController : BaseController
    {
        public FormTemplateFieldVisibilityController(IMapper mapper) : base(mapper) { }


    }
}
