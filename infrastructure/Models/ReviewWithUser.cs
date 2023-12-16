namespace Recipe_Web_App.TransferModels;

public class ReviewWithUser
{
    public int ReviewId { get; set; }
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public required string DateRated { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
}