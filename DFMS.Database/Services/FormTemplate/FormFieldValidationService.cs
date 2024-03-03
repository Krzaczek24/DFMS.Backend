using AutoMapper;
using Core.Database.Extensions;
using Core.Database.Services;
using DFMS.Database.Dto.FormTemplate;
using DFMS.Database.Models;
using KrzaqTools.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFMS.Database.Services.FormTemplate
{
    public interface IFormFieldValidationService
    {
        public Task<int> CreateValidationDefinition(FormFieldValidationRuleDefinition validationDefinition);
        public Task<bool> DeleteValidationDefinition(int id);
        public Task<ICollection<FormFieldValidationRuleDefinition>> GetValidationDefinitions(string userLogin);
        public Task<bool> UpdateValidationDefinition(int id, FormFieldValidationRuleDefinition validationDefinition);
    }

    public class FormFieldValidationService : DbService<AppDbContext>, IFormFieldValidationService
    {
        public FormFieldValidationService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public async Task<int> CreateValidationDefinition(FormFieldValidationRuleDefinition validationDefinition)
        {
            var newValidationDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinition);
            await Database.AddAsync(newValidationDefinition);
            await Database.SaveChangesAsync();
            return newValidationDefinition.Id;
        }

        public async Task<ICollection<FormFieldValidationRuleDefinition>> GetValidationDefinitions(string userLogin)
        {
            var validationDefinitions = await Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(ffvrd => ffvrd.Global == true || ffvrd.AddLogin == userLogin)
                .Include(ffvrd => ffvrd.ValidationType)
                .ToListAsync();

            var mappedDefinitions = Mapper.Map<List<FormFieldValidationRuleDefinition>>(validationDefinitions);
            return mappedDefinitions;
        }

        /// <returns><see langword="true"/> if replaced existing object, otherwise returns <see langword="false"/></returns>
        public async Task<bool> UpdateValidationDefinition(int id, FormFieldValidationRuleDefinition validationDefinition)
        {
            var dbDefinition = await Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (dbDefinition == null)
            {
                dbDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinition);
                Database.Add(dbDefinition);
                await Database.SaveChangesAsync();
                return true;
            }
            else
            {
                Mapper.Map(validationDefinition, dbDefinition);
                Database.Update(dbDefinition);
                await Database.SaveChangesAsync();
                return false;
            }
        }

        /// <returns><see langword="true"/> if object was found and removed, otherwise returns <see langword="false"/></returns>
        public async Task<bool> DeleteValidationDefinition(int id)
        {
            var dbDefinition = await Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .Where(x => x.Global.IsNotTrue())
                .SingleOrDefaultAsync();

            if (dbDefinition != null)
            {
                Database.Remove(dbDefinition);
                await Database.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
