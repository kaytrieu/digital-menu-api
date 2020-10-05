﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class BoxTypeConfiguration : IEntityTypeConfiguration<BoxType>
    {
        public void Configure(EntityTypeBuilder<BoxType> entity)
        {
            entity.Property(e => e.Description).IsUnicode(false);

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name).IsUnicode(false);
        }
    }
}