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
using System.Threading.Tasks;

namespace DFMS.WebApi.Controllers
{
    [ApiController]
    [Route("form-creator")]
    public class FormCreatorController : BaseController<FormCreatorController>
    {
        private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> _fieldValueTypes;
        private IReadOnlyDictionary<string, IReadOnlyCollection<string>> FieldValueTypes => _fieldValueTypes ??= GetFieldValueTypes();

        public FormCreatorController(ILogger<FormCreatorController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        [HttpGet("validations/definitions")]
        public async Task<ICollection<FormFieldValidationRuleDefinition>> GetValidationDefinitions()
        {
            var validationDefinitions = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(ffvrd => ffvrd.Global == true || ffvrd.AddLogin == "CREATE_SCRIPT")
                .Include(ffvrd => ffvrd.ValidationType)
                .ToListAsync()
                .ContinueWith(task => Mapper.Map<List<FormFieldValidationRuleDefinition>>(task.Result));

            return await validationDefinitions;
        }

        [HttpPost("validations/definitions")]
        public async Task<IActionResult> CreateValidationDefinition([FromBody] FormFieldValidationRuleDefinition validationDefinitions)
        {
            var newValidationDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinitions);
            await Database.AddAsync(newValidationDefinition);
            await Database.SaveChangesAsync();
            return Created(new Uri("~/validations/definitions/" + newValidationDefinition.Id), null);
        }

        [HttpPut("validations/definitions/{id:int}")]
        public async Task<IActionResult> UpdateValidationDefinition([FromRoute] int id, [FromBody] FormFieldValidationRuleDefinition validationDefinitions)
        {
            var modificationTask = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .SingleOrDefaultAsync()
                .ContinueWith(task =>
                {
                    if (task.Result == null)
                    {
                        var newValidationDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinitions);
                        Database.Add(newValidationDefinition);
                        Database.SaveChanges();
                        return newValidationDefinition.Id;
                    }
                    else
                    {
                        Mapper.Map(validationDefinitions, task.Result);
                        Database.Update(task.Result);
                        Database.SaveChanges();
                        return 0;
                    }
                });

            return await modificationTask > 0 ? Created(new Uri("~/validations/definitions/" + modificationTask.Result), null) : NoContent();
        }

        [HttpDelete("validations/definitions/{id:int}")]
        public async Task<IActionResult> DeleteValidationDefinition([FromRoute] int id)
        {
            var deletionTask = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .SingleOrDefaultAsync()
                .ContinueWith(task =>
                {
                    if (task.Result != null)
                    {
                        Database.Remove(task.Result);
                        return true;
                    }

                    return false;
                });

            return (await deletionTask) ? Ok() : NoContent();
        }

        [HttpGet("fields/definitions")]
        public async Task<ICollection<FormFieldDefinition>> GetFieldsDefinitions()
        {
            var fieldDefinitions = new List<FormFieldDefinition>();

            await Database.FormFieldDefinitions                
                .ActiveWhere(ffd => ffd.Visible == true)
                .ToListAsync()
                .ContinueWith(task =>
                {
                    Mapper.Map<List<FormFieldDefinition>>(task.Result).ForEach(x =>
                    {
                        x.ValueTypes = FieldValueTypes[x.Type];
                        fieldDefinitions.Add(x);
                    });
                });

            await Database.FormPredefiniedFields
                .ActiveWhere(fpf => fpf.Global == true || fpf.AddLogin == "CREATE_SCRIPT")
                .Include(fpf => fpf.BaseDefinition)
                .Include(fpf => fpf.ValueType)
                .ToListAsync()
                .ContinueWith(task => fieldDefinitions.AddRange(Mapper.Map<List<FormFieldDefinition>>(task.Result)));

            return fieldDefinitions.OrderBy(fd => fd.Title).ToList();
        }

        private IReadOnlyDictionary<string, IReadOnlyCollection<string>> GetFieldValueTypes()
        {
            var fieldValueTypes = Database.FormFieldDefinitionValueTypes
                .ActiveWhere()
                .Include(ffdvt => ffdvt.FieldDefinition)
                .Include(ffdvt => ffdvt.ValueType)
                .ToList()
                .GroupBy(ffdvt => ffdvt.FieldDefinition.Type)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ValueType.Code).ToList())
                .AsReadOnly();

            return fieldValueTypes;
        }
    }
}
