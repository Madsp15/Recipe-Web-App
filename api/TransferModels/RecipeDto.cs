using System.ComponentModel.DataAnnotations;
using infrastructure.Models;

namespace Recipe_Web_App.TransferModels;

public class RecipeDto
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int Servings { get; set; }
    [Required] public string Duration { get; set; }
    
    [Required] public string RecipeURL { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string Instructions { get; set; }
    public List<string> SelectedTags { get; set; }

    public List<Ingredients> Ingredients { get; set; }
}