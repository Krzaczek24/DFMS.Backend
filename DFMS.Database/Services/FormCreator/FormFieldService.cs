using AutoMapper;
using DFMS.Database.Dto;
using DFMS.Database.Extensions;
using DFMS.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.Database.Services.FormCreator
{
    public interface IFormFieldService
    {
        public ICollection<FormFieldDefinition> GetFieldsDefinitions(string userLogin);
        public bool RemovePredefiniedFieldDefinition(int id);
    }

    public class FormFieldService : DbService, IFormFieldService
    {
        private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> _fieldValueTypes;
        private IReadOnlyDictionary<string, IReadOnlyCollection<string>> FieldValueTypes => _fieldValueTypes ??= GetFieldValueTypes();

        public FormFieldService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public ICollection<FormFieldDefinition> GetFieldsDefinitions(string userLogin)
        {
            var fieldDefinitions = new List<FormFieldDefinition>();
            fieldDefinitions = null;
            fieldDefinitions.AddRange(GetBaseFormFieldDefinitions());
            fieldDefinitions.AddRange(GetPredefiniedFormFieldsDefinitions(userLogin));
            return fieldDefinitions.OrderBy(fd => fd.Title).ToList();
        }

        /// <returns><see langword="true"/> if object was found and removed, otherwise returns <see langword="false"/></returns>
        public bool RemovePredefiniedFieldDefinition(int id)
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
                    .ForEach(x => Database.Remove(x));
                Database.Remove(dbDefinition);
                Database.SaveChanges();
                return true;
            }

            return false;
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

        private List<FormFieldDefinition> GetPredefiniedFormFieldsDefinitions(string userLogin)
        {
            var predefiniedFields = Database.FormPredefiniedFields
                .ActiveWhere(fpf => fpf.Global == true || fpf.AddLogin == userLogin)
                .Include(fpf => fpf.BaseDefinition)
                .Include(fpf => fpf.ValueType)
                .ToList();

            var predefiniedMappedFields = Mapper.Map<List<FormFieldDefinition>>(predefiniedFields);

            return predefiniedMappedFields;
        }
    }
}
