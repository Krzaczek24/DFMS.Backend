using DFMS.Database.Models;
using DFMS.Database.Models.Base;
using DFMS.Shared.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.Database
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<DbDictionary> Dictionaries { get; set; }
        public virtual DbSet<DbFormFavourite> FavouriteForms { get; set; }
        public virtual DbSet<DbFormFieldDefinition> FormFieldDefinitions { get; set; }
        public virtual DbSet<DbFormFieldDefinitionValueType> FormFieldDefinitionValueTypes { get; set; }
        public virtual DbSet<DbFormFieldGroup> FormFieldGroups { get; set; }
        public virtual DbSet<DbFormFieldOption> FormFieldOptions { get; set; }
        public virtual DbSet<DbFormFieldValidationRule> FormFieldValidationRules { get; set; }
        public virtual DbSet<DbFormFieldValidationRuleDefinition> FormFieldValidationRuleDefinitions { get; set; }
        public virtual DbSet<DbFormFieldValidationRuleType> FormFieldValidationRuleTypes { get; set; }
        public virtual DbSet<DbFormFieldValidationRuleValueType> FormFieldValidationRuleValueTypes { get; set; }
        public virtual DbSet<DbFormFieldValue> FormFieldValues { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRule> FormFieldVisibilityRules { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRuleDefinition> FormFieldVisibilityRuleDefinitions { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRuleExpression> FormFieldVisibilityRuleExpressions { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRuleExpressionType> FormFieldVisibilityRuleExpressionTypes { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRuleOperator> FormFieldVisibilityRuleOperators { get; set; }
        public virtual DbSet<DbFormFieldVisibilityRulePhrase> FormFieldVisibilityRulePhrases { get; set; }
        public virtual DbSet<DbFormInstance> FormInstances { get; set; }
        public virtual DbSet<DbFormPredefiniedField> FormPredefiniedFields { get; set; }
        public virtual DbSet<DbFormPredefiniedFieldOption> FormPredefiniedFieldOptions { get; set; }
        public virtual DbSet<DbFormTemplate> FormTemplates { get; set; }
        public virtual DbSet<DbFormTemplateAccess> FormTemplateAccesss { get; set; }
        public virtual DbSet<DbFormTemplateField> FormTemplateFields { get; set; }
        public virtual DbSet<DbFormTemplateGroup> FormTemplateGroups { get; set; }
        public virtual DbSet<DbFormTemplateSection> FormTemplateSections { get; set; }
        public virtual DbSet<DbFormTemplateVersion> FormTemplateVersions { get; set; }
        public virtual DbSet<DbFormFieldValueType> FormFieldValueTypes { get; set; }
        public virtual DbSet<DbUser> Users { get; set; }
        public virtual DbSet<DbUserGroup> UserGroups { get; set; }
        public virtual DbSet<DbUserGroupMember> UserGroupMembers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is DbTableCommonModel);

            foreach (var entityEntry in entries)
            {
                var entity = entityEntry.Entity as DbTableCommonModel;

                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        if (!entity.Active.Value)
                            throw new InvalidOperationException("Cannot modify inactive records");
                        entity.ModifDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entity.Active = false;
                        entity.ModifDate = DateTime.Now;
                        entityEntry.State = EntityState.Modified;
                        break;
                }                
            }

            return base.SaveChanges();
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var tablesToMap = ReflectionToolbox.GetAllNonAbstractSubclasses(typeof(DbTableCommonModel));
            foreach (dynamic configuration in GetConfigurations(tablesToMap, modelBuilder))
            {
                modelBuilder.ApplyConfiguration(configuration);
            }
        }

        private dynamic GetConfigurations(ICollection<Type> tablesToMap, ModelBuilder modelBuilder)
        {
            var configurations = new List<dynamic>();
            var method = modelBuilder.GetType().GetMethods().Where(m => m.Name == "Entity" && m.IsGenericMethod && (m.ReturnType != m.ReflectedType)).Single();

            foreach (Type tableType in tablesToMap)
            {
                string mapTypeFullName = tableType.FullName.Replace("Models", "Mappings") + "Map";
                Type mapType = GetType().Assembly.GetType(mapTypeFullName);
                if (mapType == null)
                    throw new NotImplementedException($"There is missing [{mapTypeFullName.Split('.').Last()}] for [{tableType.Name}]");
                var builder = method.MakeGenericMethod(tableType).Invoke(modelBuilder, null);
                dynamic configurtionInstance = Activator.CreateInstance(mapType, builder);
                configurations.Add(configurtionInstance);
            }

            return configurations;
        }
    }
}
