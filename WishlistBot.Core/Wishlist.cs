namespace WishlistBot.Domain;

public class Wishlist
{
    public Guid WishlistId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedTime { get; set; }

    public User User { get; set; } = null!;
}