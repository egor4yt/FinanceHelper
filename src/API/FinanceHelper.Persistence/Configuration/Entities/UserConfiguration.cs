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
            .HasColumnType("varchar(2)");

        builder
            .Property(x => x.FirstName)
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.LastName)
            .HasColumnType("varchar(32)");

        builder
            .Property(x => x.Email)
            .HasColumnType("varchar(32)");
        
        builder
            .HasIndex(x => x.Email, "UX_Users_Email")
            .IsUnique();
        
        builder
            .Property(x => x.PasswordHash)
            .HasColumnType("varchar(64)");

        builder
            .Property(x => x.TelegramChatId)
            .HasComment("Unique telegram chat identifier")
            .HasColumnType("bigint");

        builder.HasIndex(x => x.TelegramChatId, "UX_Users_TelegramChatId")
            .IsUnique();
    }
}