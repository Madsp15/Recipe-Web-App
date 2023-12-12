using infrastructure.Models;

namespace Recipe_Web_App.TransferModels;

public class RecipeDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required int Servings { get; set; }
    public required string Duration { get; set; }
    public required int UserId { get; set; }
    public required string Instructions { get; set; }
    public List<string> SelectedTags { get; set; }
    
    public List<Ingredient> Ingredients { get; set; }
}