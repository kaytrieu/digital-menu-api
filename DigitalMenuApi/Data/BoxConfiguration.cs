﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> entity)
        {
            entity.HasOne(d => d.BoxType)
                .WithMany(p => p.Box)
                .HasForeignKey(d => d.BoxTypeId)
                .HasConstraintName("FK_Box_ContainerType");

            entity.HasOne(d => d.Footer)
                .WithMany(p => p.BoxFooter)
                .HasForeignKey(d => d.FooterId)
                .HasConstraintName("FK_Box_Session1");

            entity.HasOne(d => d.Header)
                .WithMany(p => p.BoxHeader)
                .HasForeignKey(d => d.HeaderId)
                .HasConstraintName("FK_Box_Session");

            entity.HasOne(d => d.Template)
                .WithMany(p => p.Box)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Container_Template");
        }
    }
}
