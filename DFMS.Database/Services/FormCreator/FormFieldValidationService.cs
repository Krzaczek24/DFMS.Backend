using AutoMapper;
using DFMS.Database.Dto;
using DFMS.Database.Extensions;
using DFMS.Database.Models;
using DFMS.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.Database.Services.FormCreator
{
    public interface IFormFieldValidationService
    {
        public int CreateValidationDefinition(FormFieldValidationRuleDefinition validationDefinition);
        public bool DeleteValidationDefinition(int id);
        public ICollection<FormFieldValidationRuleDefinition> GetValidationDefinitions(string userLogin);
        public bool UpdateValidationDefinition(int id, FormFieldValidationRuleDefinition validationDefinition);
    }

    public class FormFieldValidationService : DbService, IFormFieldValidationService
    {
        public FormFieldValidationService(AppDbContext database, IMapper mapper) : base(database, mapper) { }

        public int CreateValidationDefinition(FormFieldValidationRuleDefinition validationDefinition)
        {
            var newValidationDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinition);
            Database.Add(newValidationDefinition);
            Database.SaveChanges();
            return newValidationDefinition.Id;
        }

        public ICollection<FormFieldValidationRuleDefinition> GetValidationDefinitions(string userLogin)
        {
            var validationDefinitions = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(ffvrd => ffvrd.Global.IsTrue() || ffvrd.AddLogin == userLogin)
                .Include(ffvrd => ffvrd.ValidationType)
                .ToList();

            var mappedDefinitions = Mapper.Map<List<FormFieldValidationRuleDefinition>>(validationDefinitions);

            return mappedDefinitions;
        }

        /// <returns><see langword="true"/> if replaced existing object, otherwise returns <see langword="false"/></returns>
        public bool UpdateValidationDefinition(int id, FormFieldValidationRuleDefinition validationDefinition)
        {
            var dbDefinition = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .SingleOrDefault();

            if (dbDefinition == null)
            {
                dbDefinition = Mapper.Map<DbFormFieldValidationRuleDefinition>(validationDefinition);
                Database.Add(dbDefinition);
                Database.SaveChanges();
                return true;
            }
            else
            {
                Mapper.Map(validationDefinition, dbDefinition);
                Database.Update(dbDefinition);
                Database.SaveChanges();
                return false;
            }
        }

        /// <returns><see langword="true"/> if object was found and removed, otherwise returns <see langword="false"/></returns>
        public bool DeleteValidationDefinition(int id)
        {
            var dbDefinition = Database.FormFieldValidationRuleDefinitions
                .ActiveWhere(x => x.Id == id)
                .Where(x => x.Global.IsNotTrue())
                .SingleOrDefault();

            if (dbDefinition != null)
            {
                Database.Remove(dbDefinition);
                Database.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
