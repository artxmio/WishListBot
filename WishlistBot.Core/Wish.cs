namespace WishlistBot.Core;

public class Wish
{
    public Guid WishId { get; set; }
    public Guid WishlistId { get; set; }
    public string Title { get; set; } = null!;
    public string? Url { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreatedTime { get; set; }
}