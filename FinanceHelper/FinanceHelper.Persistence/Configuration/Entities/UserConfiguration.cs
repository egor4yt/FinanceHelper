using FinanceHelper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHelper.Persistence.Configuration.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasOne(x => x.PreferredLocalization)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.PreferredLocalizationCode)
            .HasConstraintName("FK_User_SupportedLocalization");

        builder
            .Property(x => x.PreferredLocalizationCode)
            .IsRequired()
            .HasColumnType("varchar(2)");

        builder
            .Property(x => x.FirstName)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.LastName)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.PasswordHash)
            .IsRequired();
    }
}