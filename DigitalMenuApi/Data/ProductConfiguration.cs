
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

namespace DigitalMenuApi.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasIndex(e => e.StoreId);

            entity.Property(e => e.Description).HasDefaultValueSql("('')");

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Price).HasColumnType("smallmoney");

            entity.Property(e => e.Src).HasDefaultValueSql("('')");

            entity.Property(e => e.Title).HasDefaultValueSql("('')");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Product_Store");
        }
    }
}
