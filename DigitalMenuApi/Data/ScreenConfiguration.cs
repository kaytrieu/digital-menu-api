
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class ScreenConfiguration : IEntityTypeConfiguration<Screen>
    {
        public void Configure(EntityTypeBuilder<Screen> entity)
        {
            entity.HasIndex(e => e.StoreId);

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Uid)
                .IsRequired()
                .HasColumnName("UId");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Screen)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Screen_Store");
        }
    }
}
