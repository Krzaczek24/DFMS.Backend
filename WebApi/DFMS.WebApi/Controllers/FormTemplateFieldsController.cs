using AutoMapper;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Services.FormTemplate;
using DFMS.WebApi.Common.Attributes;
using DFMS.WebApi.Common.Controllers;
using DFMS.WebApi.Common.Exceptions;
using DFMS.WebApi.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [ApiRoute("form-template/field-definitions")]
    public class FormTemplateFieldsController(IMapper mapper, IFormFieldService formFieldService) : ResponseController(mapper)
    {
        private IFormFieldService FormFieldService { get; } = formFieldService;

        [HttpGet]
        public async Task<ICollection<FormFieldDefinitionDto>> GetFieldsDefinitions()
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
