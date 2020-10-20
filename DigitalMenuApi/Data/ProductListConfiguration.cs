
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class ProductListConfiguration : IEntityTypeConfiguration<ProductList>
    {
        public void Configure(EntityTypeBuilder<ProductList> entity)
        {
            entity.HasIndex(e => e.BoxId);

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Title).HasDefaultValueSql("('')");

            entity.HasOne(d => d.Box)
                .WithMany(p => p.ProductList)
                .HasForeignKey(d => d.BoxId)
                .HasConstraintName("FK_ProductList_Box");
        }
    }
}
