﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> entity)
        {
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");

            entity.Property(e => e.Description).IsRequired();

            entity.Property(e => e.Uilink).HasColumnName("UILink");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Template)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Template_Store");
        }
    }
}
