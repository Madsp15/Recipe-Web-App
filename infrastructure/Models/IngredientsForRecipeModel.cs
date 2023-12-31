namespace infrastructure.Models;

public class Ingredients
{
    public int IngredientId { get; set; }
    
    public int RecipeIngredientId { get; set; }
    
    public string IngredientName { get; set; }
    
    public int RecipeId { get; set; }
    
    public int Quantity { get; set; }
    
    public string Unit { get; set; }
}