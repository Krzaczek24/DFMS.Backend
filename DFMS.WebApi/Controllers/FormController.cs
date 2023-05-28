﻿using AutoMapper;
using Core.WebApi.Controllers;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route(Routes.Form)]
    public class FormController : BaseResponseController
    {
        public FormController(IMapper mapper) : base(mapper) { }
    }
}