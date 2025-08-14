using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishlistBot.Domain;

namespace WishlistBot.Persistanse.EntityTypeConfiguration;

public class WishlistTypeConfiguration
    : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.ToTable("Wishlists");
        builder.HasKey(wl => wl.WishlistId);
        builder.Property(wl => wl.Title)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(wl => wl.CreatedTime)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(wl => wl.User)
            .WithMany(u => u.Wishlists)
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}