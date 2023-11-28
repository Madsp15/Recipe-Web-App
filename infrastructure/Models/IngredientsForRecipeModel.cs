namespace infrastructure.Models;

public class IngredientsForRecipe
{
    public int IngredientId { get; set; }
    
    public int RecipeIngredientId { get; set; }
    
    public String IngredientName { get; set; }
    
    public int RecipeId { get; set; }
    
    public int Quantity { get; set; }
    
    public string Unit { get; set; }
    
    
}