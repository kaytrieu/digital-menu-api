
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class LayoutConfiguration : IEntityTypeConfiguration<Layout>
    {
        public void Configure(EntityTypeBuilder<Layout> entity)
        {
            entity.Property(e => e.AspectRatio)
                .IsRequired()
                .HasMaxLength(50)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasDefaultValueSql("('')");
        }
    }
}
