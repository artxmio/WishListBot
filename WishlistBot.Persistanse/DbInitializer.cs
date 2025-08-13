namespace WishlistBot.Persistanse;

public class DbInitializer
{
    public static void Initialize(WishlistBotDbContext context)
    {
        context.Database.EnsureCreated();
    }
}