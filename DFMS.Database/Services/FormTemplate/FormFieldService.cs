using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Dto.FormTemplate;
using KrzaqTools.NullableBooleanExtension;
using KrzaqTools.ReadOnlyDictionaryExtension;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services.FormTemplate
{
    public interface IFormFieldService
    {
        public Task<ICollection<FormFieldDefinition>> GetFieldsDefinitions(string userLogin);
        public Task<bool> RemovePredefiniedFieldDefinition(int id);
    }

    public class FormFieldService : DbService<AppDbContext>, IFormFieldService
    {
        private static IReadOnlyDictionary<string, IReadOnlyCollection<string>> _fieldValueTypes;
        private IReadOnlyDictionary<string, IReadOnlyCollection<string>> FieldValueTypes => _fieldValueTypes ??= GetFieldValueTypes().Result;

        public FormFieldService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public async Task<ICollection<FormFieldDefinition>> GetFieldsDefinitions(string userLogin)
        {
            var fieldDefinitions = new List<FormFieldDefinition>();
            fieldDefinitions.AddRange(await GetBaseFormFieldDefinitions());
            fieldDefinitions.AddRange(await GetPredefiniedFormFieldsDefinitions(userLogin));
            return fieldDefinitions.OrderBy(fd => fd.Title).ToList();
        }

        /// <returns><see langword="true"/> if object was found and removed, otherwise returns <see langword="false"/></returns>
        public async Task<bool> RemovePredefiniedFieldDefinition(int id)
        {
            var dbDefinition = await Database.FormPredefiniedFields
                .ActiveWhere(x => x.Id == id)
                .Where(x => x.Global.IsNotTrue())
                .SingleOrDefaultAsync();

            if (dbDefinition != null)
            {
                (await Database.FormPredefiniedFieldOptions
                    .ActiveWhere(x => x.PredefiniedField == dbDefinition)
                    .ToListAsync())
                    .ForEach(x => Database.Remove(x));
                Database.Remove(dbDefinition);
                await Database.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<IReadOnlyDictionary<string, IReadOnlyCollection<string>>> GetFieldValueTypes()
        {
            var fieldValueTypes = (await Database.FormFieldDefinitionValueTypes
                .ActiveWhere()
                .Include(ffdvt => ffdvt.FieldDefinition)
                .Include(ffdvt => ffdvt.ValueType)
                .ToListAsync())
                .GroupBy(ffdvt => ffdvt.FieldDefinition.Type)
                .ToDictionary(x => x.Key, x => x.Select(y => y.ValueType.Code).ToList())
                .AsReadOnly();

            return fieldValueTypes;
        }

        private async Task<List<FormFieldDefinition>> GetBaseFormFieldDefinitions()
        {
            var baseFields = await Database.FormFieldDefinitions
                .ActiveWhere(ffd => ffd.Visible == true)
                .ToListAsync();

            var mappedFields = Mapper.Map<List<FormFieldDefinition>>(baseFields);

            foreach (var field in mappedFields)
            {
                field.ValueTypes = FieldValueTypes[field.Type];
            }

            return mappedFields;
        }

        private async Task<List<FormFieldDefinition>> GetPredefiniedFormFieldsDefinitions(string userLogin)
        {
            var predefiniedFields = await Database.FormPredefiniedFields
                .ActiveWhere(fpf => fpf.Global == true || fpf.AddLogin == userLogin)
                .Include(fpf => fpf.BaseDefinition)
                .Include(fpf => fpf.ValueType)
                .ToListAsync();

            var predefiniedMappedFields = Mapper.Map<List<FormFieldDefinition>>(predefiniedFields);

            return predefiniedMappedFields;
        }
    }
}
