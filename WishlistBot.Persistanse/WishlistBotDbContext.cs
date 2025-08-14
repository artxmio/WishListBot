using Microsoft.EntityFrameworkCore;
using WishlistBot.Application.Interfaces;
using WishlistBot.Domain;
using WishlistBot.Persistanse.EntityTypeConfiguration;

namespace WishlistBot.Persistanse;

public class WishlistBotDbContext(DbContextOptions<WishlistBotDbContext> options)
    : DbContext(options), IWishlistBotDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Wishlist> Wishlists { get; set; } = null!;
    public DbSet<Wish> Wishes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WishlistTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WishTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
