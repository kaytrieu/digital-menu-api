
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class ScreenTemplateConfiguration : IEntityTypeConfiguration<ScreenTemplate>
    {
        public void Configure(EntityTypeBuilder<ScreenTemplate> entity)
        {
            entity.HasIndex(e => e.ScreenId);

            entity.HasIndex(e => e.TemplateId);

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ScreenId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Screen)
                .WithMany(p => p.ScreenTemplate)
                .HasForeignKey(d => d.ScreenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScreenTemplate_Screen");

            entity.HasOne(d => d.Template)
                .WithMany(p => p.ScreenTemplate)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScreenTemplate_Template");
        }
    }
}
