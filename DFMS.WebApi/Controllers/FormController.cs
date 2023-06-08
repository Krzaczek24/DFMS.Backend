using AutoMapper;
using DFMS.WebApi.Constants;
using DFMS.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route(Routes.Form)]
    public class FormController : ResponseController
    {
        public FormController(IMapper mapper) : base(mapper) { }
    }
}