﻿using DFMS.Database.Models;
using DFMS.Database.Models.Base;
using KrzaqTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DFMS.Database
{
    public class DfmsDbContext : DbContext
    {
        private static readonly string[] UNMODIFIABLE_PROPERTIES = [""];

        internal virtual DbSet<DbDictionary> Dictionaries { get; set; }
        internal virtual DbSet<DbFormFavourite> FavouriteForms { get; set; }
        internal virtual DbSet<DbFormFieldDefinition> FormFieldDefinitions { get; set; }
        internal virtual DbSet<DbFormFieldDefinitionValueType> FormFieldDefinitionValueTypes { get; set; }
        internal virtual DbSet<DbFormFieldGroup> FormFieldGroups { get; set; }
        internal virtual DbSet<DbFormFieldOption> FormFieldOptions { get; set; }
        internal virtual DbSet<DbFormFieldValidationRule> FormFieldValidationRules { get; set; }
        internal virtual DbSet<DbFormFieldValidationRuleDefinition> FormFieldValidationRuleDefinitions { get; set; }
        internal virtual DbSet<DbFormFieldValidationRuleType> FormFieldValidationRuleTypes { get; set; }
        internal virtual DbSet<DbFormFieldValidationRuleValueType> FormFieldValidationRuleValueTypes { get; set; }
        internal virtual DbSet<DbFormFieldValue> FormFieldValues { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRule> FormFieldVisibilityRules { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRuleDefinition> FormFieldVisibilityRuleDefinitions { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRuleExpression> FormFieldVisibilityRuleExpressions { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRuleExpressionType> FormFieldVisibilityRuleExpressionTypes { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRuleOperator> FormFieldVisibilityRuleOperators { get; set; }
        internal virtual DbSet<DbFormFieldVisibilityRulePhrase> FormFieldVisibilityRulePhrases { get; set; }
        internal virtual DbSet<DbFormInstance> FormInstances { get; set; }
        internal virtual DbSet<DbFormPredefiniedField> FormPredefiniedFields { get; set; }
        internal virtual DbSet<DbFormPredefiniedFieldOption> FormPredefiniedFieldOptions { get; set; }
        internal virtual DbSet<DbFormTemplate> FormTemplates { get; set; }
        internal virtual DbSet<DbFormTemplateAccess> FormTemplateAccesss { get; set; }
        internal virtual DbSet<DbFormTemplateField> FormTemplateFields { get; set; }
        internal virtual DbSet<DbFormTemplateSection> FormTemplateSections { get; set; }
        internal virtual DbSet<DbFormTemplateVersion> FormTemplateVersions { get; set; }
        internal virtual DbSet<DbFormFieldValueType> FormFieldValueTypes { get; set; }
        internal virtual DbSet<DbUser> Users { get; set; }
        internal virtual DbSet<DbUserGroup> UserGroups { get; set; }
        internal virtual DbSet<DbUserGroupMember> UserGroupMembers { get; set; }
        internal virtual DbSet<DbUserPermission> UserPermissions { get; set; }
        internal virtual DbSet<DbUserPermissionAssignment> UserPermissionAssignments { get; set; }
        internal virtual DbSet<DbUserPermissionGroup> UserPermissionGroups { get; set; }
        internal virtual DbSet<DbUserPermissionGroupAssignment> UserPermissionGroupAssignments { get; set; }
        internal virtual DbSet<DbUserRole> UserRoles { get; set; }
        internal virtual DbSet<DbUserSession> UserSessions { get; set; }
        internal virtual DbSet<DbWorkspace> Workspaces { get; set; }
        internal virtual DbSet<DbWorkspaceUser> WorkspaceUsers { get; set; }

        public DfmsDbContext(DbContextOptions<DfmsDbContext> options) : base(options) { }

        public override sealed int SaveChanges()
        {
            ValidateAndSetModifDate();
            return base.SaveChanges();
        }

        public override sealed Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ValidateAndSetModifDate();
            return base.SaveChangesAsync(cancellationToken);
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
                string mapTypeFullName = tableType.FullName!.Replace("Models", "Mappings") + "Map";
                Type mapType = GetType().Assembly.GetType(mapTypeFullName)!;
                if (mapType == null)
                    throw new NotImplementedException($"There is missing [{mapTypeFullName.Split('.').Last()}] for [{tableType.Name}]");
                var builder = method.MakeGenericMethod(tableType).Invoke(modelBuilder, null);
                dynamic configurtionInstance = Activator.CreateInstance(mapType, builder);
                configurations.Add(configurtionInstance);
            }

            return configurations;
        }

        private void ValidateAndSetModifDate()
        {
            DbTableCommonModel entity;

            foreach (var entityEntry in ChangeTracker.Entries().Where(e => e.Entity is DbTableCommonModel))
            {
                entity = (DbTableCommonModel)entityEntry.Entity;

                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        if (entity.Active.HasValue && !entity.Active.Value)
                            throw new InvalidOperationException("Cannot modify inactive records");
                        foreach (string member in DbTableCommonModel.UnmodifiableMembers)
                            if (entityEntry.Member(member).IsModified)
                                throw new InvalidOperationException($"Cannot modify '{member}' member");
                        entity.ModifDate = DateTime.Now;
                        break;
                    //case EntityState.Deleted:
                    //    entity.Active = false;
                    //    entity.ModifDate = DateTime.Now;
                    //    break;
                }
            }
        }
    }
}
