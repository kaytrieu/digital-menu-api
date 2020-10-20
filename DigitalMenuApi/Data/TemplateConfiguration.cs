
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
            entity.HasIndex(e => e.StoreId);

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");

            entity.Property(e => e.Description).HasDefaultValueSql("('')");

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.LastModified).HasColumnType("datetime");

            entity.Property(e => e.Tags).HasDefaultValueSql("('')");

            entity.Property(e => e.Uilink)
                .HasColumnName("UILink")
                .HasDefaultValueSql("('')");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Template)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Template_Store");
        }
    }
}
