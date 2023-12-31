namespace infrastructure;

public class Recipe
{
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Instructions { get; set; }
    public string? RecipeURL { get; set; }
    public string DateCreated { get; set; }
    public string? Duration { get; set; }
    public int Servings { get; set; }

}