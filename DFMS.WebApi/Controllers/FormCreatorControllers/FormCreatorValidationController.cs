using AutoMapper;
using DFMS.Database.Dto;
using DFMS.Database.Services.FormCreator;
using DFMS.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [Authorize]
    [ApiController]
    [Route("form-creator/validation-rules")]
    public class FormCreatorValidationController : BaseController
    {
        private IFormFieldValidationService FormFieldValidationService { get; }

        public FormCreatorValidationController(IMapper mapper, IFormFieldValidationService formFieldValidationService) : base(mapper)
        {
            FormFieldValidationService = formFieldValidationService;
        }

        [HttpGet]
        public ICollection<FormFieldValidationRuleDefinition> GetValidationDefinitions()
        {
            return FormFieldValidationService.GetValidationDefinitions(User.GetLogin());
        }

        [HttpPost]
        public IActionResult CreateValidationDefinition([FromBody] FormFieldValidationRuleDefinition validationDefinition)
        {
            int newValidationDefinitionId = FormFieldValidationService.CreateValidationDefinition(validationDefinition);
            return Created(new Uri("~/validations/definitions/" + newValidationDefinitionId), null);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateValidationDefinition([FromRoute] int id, [FromBody] FormFieldValidationRuleDefinition validationDefinition)
        {
            bool replaced = FormFieldValidationService.UpdateValidationDefinition(id, validationDefinition);
            return replaced ? NoContent() : Created(new Uri("~/validations/definitions/" + id), null);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteValidationDefinition([FromRoute] int id)
        {
            bool removed = FormFieldValidationService.DeleteValidationDefinition(id);
            return removed ? Ok() : NoContent();
        }
    }
}
