﻿using AutoMapper;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Services.FormTemplate;
using DFMS.WebApi.Authorization;
using DFMS.WebApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route(Routes.FormTemplate + "/validation-rules")]
    public class FormTemplateFieldValidationController : BaseController
    {
        private IFormFieldValidationService FormFieldValidationService { get; }

        public FormTemplateFieldValidationController(IMapper mapper, IFormFieldValidationService formFieldValidationService) : base(mapper)
        {
            FormFieldValidationService = formFieldValidationService;
        }

        [HttpGet]
        public async Task<ICollection<FormFieldValidationRuleDefinition>> GetValidationDefinitions()
        {
            return await FormFieldValidationService.GetValidationDefinitions(User.GetLogin());
        }

        [HttpPost]
        public async Task<IActionResult> CreateValidationDefinition([FromBody] FormFieldValidationRuleDefinition validationDefinition)
        {
            int newValidationDefinitionId = await FormFieldValidationService.CreateValidationDefinition(validationDefinition);
            return Created(new Uri("~/validations/definitions/" + newValidationDefinitionId), null);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateValidationDefinition([FromRoute] int id, [FromBody] FormFieldValidationRuleDefinition validationDefinition)
        {
            bool replaced = await FormFieldValidationService.UpdateValidationDefinition(id, validationDefinition);
            return replaced ? NoContent() : Created(new Uri("~/validations/definitions/" + id), null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteValidationDefinition([FromRoute] int id)
        {
            bool removed = await FormFieldValidationService.DeleteValidationDefinition(id);
            return removed ? Ok() : NoContent();
        }
    }
}