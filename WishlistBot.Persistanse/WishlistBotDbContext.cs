using Microsoft.EntityFrameworkCore;
using WishlistBot.Application.Interfaces;
using WishlistBot.Core;
using WishlistBot.Persistanse.EntityTypeConfiguration;

namespace WishlistBot.Persistanse;

public class WishlistBotDbContext(DbContextOptions<WishlistBotDbContext> options)
    : DbContext(options), IWishlistBotDbContext
{
    public DbSet<User> Users { get; } = null!;
    public DbSet<Wishlist> Wishlists { get; } = null!;
    public DbSet<Wish> Wishes { get; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WishlistTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WishTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
