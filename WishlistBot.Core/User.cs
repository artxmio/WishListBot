namespace WishlistBot.Core;

public class User
{
    public Guid UserId { get; set; }
    public long TelegramId { get; set; }
    public string UserName { get; set; } = null!;
    public DateTime CreatedTime { get; set; }

    public IEnumerable<Wishlist> Wishlists { get; set; } = null!;
}