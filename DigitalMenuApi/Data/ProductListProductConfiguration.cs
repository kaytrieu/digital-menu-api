
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class ProductListProductConfiguration : IEntityTypeConfiguration<ProductListProduct>
    {
        public void Configure(EntityTypeBuilder<ProductListProduct> entity)
        {
            entity.HasIndex(e => e.ProductId);

            entity.HasIndex(e => e.ProductListId);

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductListProduct)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductListProduct_Product");

            entity.HasOne(d => d.ProductList)
                .WithMany(p => p.ProductListProduct)
                .HasForeignKey(d => d.ProductListId)
                .HasConstraintName("FK_ProductListProduct_ProductList");
        }
    }
}
