using Microsoft.EntityFrameworkCore;
using WishlistBot.Core;

namespace WishlistBot.Application.Interfaces;

public interface IWishlistBotDbContext
{
    DbSet<User> Users { get; }
    DbSet<Wishlist> Wishlists { get; }
    DbSet<Wish> Wishes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
