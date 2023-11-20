namespace infrastructure;

public class Review
{
    public int ReviewId { get; set; }
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public string Date { get; set; }
}