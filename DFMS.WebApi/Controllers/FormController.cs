using AutoMapper;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route(Routes.Form)]
    public class FormController : BaseController
    {
        public FormController(IMapper mapper) : base(mapper) { }
    }
}