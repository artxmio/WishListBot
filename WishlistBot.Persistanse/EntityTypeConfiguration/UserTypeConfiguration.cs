using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishlistBot.Domain;

namespace WishlistBot.Persistanse.EntityTypeConfiguration;

public class UserTypeConfiguration
    : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(u => u.TelegramUserId)
            .IsRequired();
        builder.Property(u => u.CreatedTime)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(u => u.Wishlists)
            .WithOne(wl => wl.User)
            .HasForeignKey(u => u.UserId);
    }
}