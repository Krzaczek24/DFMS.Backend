﻿using DFMS.Database.Mappings.Base;
using DFMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFMS.Database.Mappings
{
    internal class DbFormFieldValidationRuleMap : DbTableCommonModelMap<DbFormFieldValidationRule>
    {
        public DbFormFieldValidationRuleMap(EntityTypeBuilder<DbFormFieldValidationRule> builder) : base(builder) { }

        public override void Configure(EntityTypeBuilder<DbFormFieldValidationRule> builder)
        {
            builder.ToTable("form_field_validation_rule");

            builder.HasOne(e => e.Field)
                .WithMany()
                .HasForeignKey("field_id")
                .HasConstraintName("fk_ffvr_field")
                .IsRequired();

            builder.HasOne(e => e.ValidationRule)
                .WithMany()
                .HasForeignKey("validation_rule_id")
                .HasConstraintName("fk_ffvr_validation_rule")
                .IsRequired();

            builder.Property(e => e.Value)
                .HasColumnName("value")
                .HasMaxLength(256);

            builder.Property(e => e.Message)
                .HasColumnName("message")
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
