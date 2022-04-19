using AutoMapper;
using DFMS.Database.Dto;
using DFMS.Database.Services.FormCreator;
using DFMS.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    //[Authorize]
    [ApiController]
    [Route("form-creator/field-definitions")]
    public class FormCreatorFieldsController : BaseController
    {
        private IFormFieldService FormFieldService { get; }

        public FormCreatorFieldsController(IMapper mapper, IFormFieldService formFieldService) : base(mapper)
        {
            FormFieldService = formFieldService;
        }

        [HttpGet]
        public ICollection<FormFieldDefinition> GetFieldsDefinitions() => FormFieldService.GetFieldsDefinitions(string.Empty);//User.GetLogin()

        [HttpPost]
        public IActionResult CreatePredefiniedFieldDefinition()
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePredefiniedFieldDefinition([FromRoute] int id)
        {
            bool removed = FormFieldService.RemovePredefiniedFieldDefinition(id);
            return removed ? Ok() : NoContent();
        }
    }
}
