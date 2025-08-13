using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WishlistBot.Core;

namespace WishlistBot.Persistanse.EntityTypeConfiguration;

public class WishTypeConfiguration 
    : IEntityTypeConfiguration<Wish>
{
    public void Configure(EntityTypeBuilder<Wish> builder)
    {
        builder.ToTable("Wishes");
        builder.HasKey(w => w.WishId);
        builder.Property(w => w.Title)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(w => w.Url)
            .HasMaxLength(500);
        builder.Property(w => w.Price)
            .HasColumnType("decimal(18,2)");
        builder.Property(w => w.CreatedTime)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.HasOne<Wishlist>()
            .WithMany()
            .HasForeignKey(w => w.WishlistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}