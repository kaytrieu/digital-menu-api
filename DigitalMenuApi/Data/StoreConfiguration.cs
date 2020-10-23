
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> entity)
        {
            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasDefaultValueSql("('')");
        }
    }
}
