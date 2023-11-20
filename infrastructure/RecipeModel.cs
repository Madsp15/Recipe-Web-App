namespace infrastructure;

public class Recipe
{
    public int RecipeId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Instuctions { get; set; }
    public string? RecipeURL { get; set; }
    public string DateCreated { get; set; }
    public string? Notes { get; set; }

}