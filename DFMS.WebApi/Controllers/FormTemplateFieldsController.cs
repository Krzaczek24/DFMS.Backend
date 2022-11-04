using AutoMapper;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Services.FormTemplate;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate + "/field-definitions")]
    public class FormTemplateFieldsController : BaseController
    {
        private IFormFieldService FormFieldService { get; }

        public FormTemplateFieldsController(IMapper mapper, IFormFieldService formFieldService) : base(mapper)
        {
            FormFieldService = formFieldService;
        }

        [HttpGet]
        public async Task<ICollection<FormFieldDefinition>> GetFieldsDefinitions()
        {
            return await FormFieldService.GetFieldsDefinitions(string.Empty);//User.GetLogin()
        }

        [HttpPost]
        public async Task<IActionResult> CreatePredefiniedFieldDefinition()
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredefiniedFieldDefinition([FromRoute] int id)
        {
            bool removed = await FormFieldService.RemovePredefiniedFieldDefinition(id);
            return removed ? Ok() : NoContent();
        }
    }
}
