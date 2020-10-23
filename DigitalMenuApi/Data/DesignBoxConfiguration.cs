
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class DesignBoxConfiguration : IEntityTypeConfiguration<DesignBox>
    {
        public void Configure(EntityTypeBuilder<DesignBox> entity)
        {
            entity.Property(e => e.Height).HasColumnType("decimal(3, 2)");

            entity.Property(e => e.Width).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.Layout)
                .WithMany(p => p.DesignBox)
                .HasForeignKey(d => d.LayoutId)
                .HasConstraintName("FK_DesignBox_Layout");
        }
    }
}
