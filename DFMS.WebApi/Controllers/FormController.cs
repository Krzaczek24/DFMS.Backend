﻿using AutoMapper;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("form")]
    public class FormController(IMapper mapper) : ResponseController(mapper)
    {
    }
}