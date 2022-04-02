using AutoMapper;
using DFMS.Database;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Dto;
using DFMS.Shared.Extensions;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [ApiController]
    [Route("form-creator/validation-rules")]
    public class FormCreatorValidationController : BaseController<FormCreatorValidationController>
    {
        public FormCreatorValidationController(ILogger<FormCreatorValidationController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        [HttpGet]
        public ICollection<FormFieldValidationRuleDefinition> GetValidationDefinitions()
        {
            var validationDefinitions = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(ffvrd => ffvrd.Global.IsTrue() || ffvrd.AddLogin == UserLogin)
                .Include(ffvrd => ffvrd.ValidationType)
                .ToList();

            var mappedDefinitions = Mapper.Map<List<FormFieldValidationRuleDefinition>>(validationDefinitions);

            return mappedDefinitions;
        }

        [HttpPost]
        public IActionResult CreateValidationDefinition([FromBody] FormFieldValidationRuleDefinition validationDefinitions)
        {
            var newValidationDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinitions);
            Database.Add(newValidationDefinition);
            Database.SaveChanges();
            return Created(new Uri("~/validations/definitions/" + newValidationDefinition.Id), null);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateValidationDefinition([FromRoute] int id, [FromBody] FormFieldValidationRuleDefinition validationDefinition)
        {
            var dbDefinition = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .SingleOrDefault();

            if (dbDefinition == null)
            {
                dbDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinition);
                Database.Add(dbDefinition);
                Database.SaveChanges();
                return Created(new Uri("~/validations/definitions/" + dbDefinition.Id), null);
            }
            else
            {
                Mapper.Map(validationDefinition, dbDefinition);
                Database.Update(dbDefinition);
                Database.SaveChanges();
                return NoContent();
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteValidationDefinition([FromRoute] int id)
        {
            var dbDefinition = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .Where(x => x.Global.IsNotTrue())
                .SingleOrDefault();

            if (dbDefinition != null)
            {
                Database.Remove(dbDefinition);
                Database.SaveChanges();
                return Ok();
            }

            return NoContent();
        }
    }
}
