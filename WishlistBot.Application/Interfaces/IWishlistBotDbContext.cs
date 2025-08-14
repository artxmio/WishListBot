using Microsoft.EntityFrameworkCore;
using WishlistBot.Domain;

namespace WishlistBot.Application.Interfaces;

public interface IWishlistBotDbContext
{
    DbSet<User> Users { get; }
    DbSet<Wishlist> Wishlists { get; }
    DbSet<Wish> Wishes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
