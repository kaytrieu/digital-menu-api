
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class BoxConfiguration : IEntityTypeConfiguration<Box>
    {
        public void Configure(EntityTypeBuilder<Box> entity)
        {
            entity.HasIndex(e => e.BoxTypeId);

            entity.HasIndex(e => e.TemplateId);

            entity.Property(e => e.FooterSrc).HasDefaultValueSql("('')");

            entity.Property(e => e.FooterTitle).HasDefaultValueSql("('')");

            entity.Property(e => e.HeaderSrc).HasDefaultValueSql("('')");

            entity.Property(e => e.HeaderTitle).HasDefaultValueSql("('')");

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Src).HasDefaultValueSql("('')");

            entity.HasOne(d => d.BoxType)
                .WithMany(p => p.Box)
                .HasForeignKey(d => d.BoxTypeId)
                .HasConstraintName("FK_Box_ContainerType");

            entity.HasOne(d => d.Template)
                .WithMany(p => p.Box)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Container_Template");
        }
    }
}
