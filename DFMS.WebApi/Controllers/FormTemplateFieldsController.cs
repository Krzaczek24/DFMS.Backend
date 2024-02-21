using AutoMapper;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Services.FormTemplate;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Core.Attributes;
using DFMS.WebApi.Core.Controllers;
using DFMS.WebApi.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("form-template/field-definitions")]
    public class FormTemplateFieldsController : ResponseController
    {
        private IFormFieldService FormFieldService { get; }

        public FormTemplateFieldsController(IMapper mapper, IFormFieldService formFieldService) : base(mapper)
        {
            FormFieldService = formFieldService;
        }

        [HttpGet]
        public async Task<ICollection<FormFieldDefinition>> GetFieldsDefinitions()
        {
            return await FormFieldService.GetFieldsDefinitions(User.GetLogin());
        }

        [HttpPost]
        public async Task<IActionResult> CreatePredefiniedFieldDefinition()
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task DeletePredefiniedFieldDefinition([FromRoute] int id)
        {
            if (!await FormFieldService.RemovePredefiniedFieldDefinition(id))
                throw new NotFoundException();
        }
    }
}
