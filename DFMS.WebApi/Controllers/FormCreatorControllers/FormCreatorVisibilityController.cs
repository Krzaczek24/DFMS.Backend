using AutoMapper;
using DFMS.Database;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Dto;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [ApiController]
    [Route("form-creator/visibility-rules")]
    public class FormCreatorVisibilityController : BaseController<FormCreatorVisibilityController>
    {
        public FormCreatorVisibilityController(ILogger<FormCreatorVisibilityController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        
    }
}
