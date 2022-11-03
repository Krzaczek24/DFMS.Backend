using AutoMapper;
using DFMS.Database.Services;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.Privilege)]
    public class PrivilegeController : BaseController
    {
        private IPrivilegeService PrivilegeService { get; }

        public PrivilegeController(IMapper mapper, IPrivilegeService privilegeService) : base(mapper)
        {
            PrivilegeService = privilegeService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetPrivileges()
        {
            return null;
        }
    }
}
