
using DigitalMenuApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalMenuApi.Data
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.HasIndex(e => e.RoleId);

            entity.HasIndex(e => e.StoreId);
            entity.HasIndex(entity => entity.Username).IsUnique();

            entity.Property(e => e.IsAvailable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.Password)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Salt)
                .IsRequired()
                .IsUnicode(false).HasMaxLength(50);


            entity.Property(e => e.Username)
                .IsRequired()
                .IsUnicode(false).HasMaxLength(100);

            entity.HasOne(d => d.Role)
                .WithMany(p => p.Account)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_AcountRole");

            entity.HasOne(d => d.Store)
                .WithMany(p => p.Account)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Account_Store");
        }
    }
}
