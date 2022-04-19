using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("form")]
    public class FormController : BaseController
    {
        public FormController(IMapper mapper) : base(mapper) { }

        
    }
}