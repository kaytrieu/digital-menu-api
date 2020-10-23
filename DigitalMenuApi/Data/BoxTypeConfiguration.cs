
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class BoxTypeConfiguration : IEntityTypeConfiguration<BoxType>
    {
        public void Configure(EntityTypeBuilder<BoxType> entity)
        {
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name)
                .IsUnicode(false)
                .HasDefaultValueSql("('')");
        }
    }
}
