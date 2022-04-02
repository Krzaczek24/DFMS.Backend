using AutoMapper;
using DFMS.Database;
using DFMS.Database.Extensions;
using DFMS.Shared.Dto;
using DFMS.Shared.Extensions;
using DFMS.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.WebApi.Controllers.FormCreatorControllers
{
    [ApiController]
    [Route("form-creator/field-definitions")]
    public class FormCreatorFieldsController : BaseController<FormCreatorFieldsController>
    {
        private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> _fieldValueTypes;
        private IReadOnlyDictionary<string, IReadOnlyCollection<string>> FieldValueTypes => _fieldValueTypes ??= GetFieldValueTypes();

        public FormCreatorFieldsController(ILogger<FormCreatorFieldsController> logger, IMapper mapper, AppDbContext database) : base(logger, mapper, database) { }

        [HttpGet]
        public ICollection<FormFieldDefinition> GetFieldsDefinitions()
        {
            var fieldDefinitions = new List<FormFieldDefinition>();
            fieldDefinitions.AddRange(GetBaseFormFieldDefinitions());
            fieldDefinitions.AddRange(GetPredefiniedFormFieldsDefinitions());
            return fieldDefinitions.OrderBy(fd => fd.Title).ToList();
        }

        [HttpPost]
        public IActionResult CreatePredefiniedFieldDefinition()
        {
            throw new System.NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePredefiniedFieldDefinition([FromRoute] int id)
        {
            var dbDefinition = Database.FormPredefiniedFields
                .ActiveWhere(x => x.Id == id)
                .Where(x => x.Global.IsNotTrue())
                .SingleOrDefault();

            if (dbDefinition != null)
            {
                Database.FormPredefiniedFieldOptions
                    .ActiveWhere(x => x.PredefiniedField == dbDefinition)
                    .ToList()
                    .ForEach(option => Database.Remove(option));
                Database.Remove(dbDefinition);
                Database.SaveChanges();
                return Ok();
            }

            return NoContent();
        }

        private List<FormFieldDefinition> GetBaseFormFieldDefinitions()
        {
            var baseFields = Database.FormFieldDefinitions
                .ActiveWhere(ffd => ffd.Visible == true)
                .ToList();

            var mappedFields = Mapper.Map<List<FormFieldDefinition>>(baseFields);

            foreach (var field in mappedFields)
            {
                field.ValueTypes = FieldValueTypes[field.Type];
            }

            return mappedFields;
        }

        private List<FormFieldDefinition> GetPredefiniedFormFieldsDefinitions()
        {
            var predefiniedFields = Database.FormPredefiniedFields
                .ActiveWhere(fpf => fpf.Global == true || fpf.AddLogin == UserLogin)
                .Include(fpf => fpf.BaseDefinition)
                .Include(fpf => fpf.ValueType)
                .ToList();

            var predefiniedMappedFields = Mapper.Map<List<FormFieldDefinition>>(predefiniedFields);

            return predefiniedMappedFields;
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
