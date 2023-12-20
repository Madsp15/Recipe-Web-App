namespace infrastructure.Models;

public class RecipeIngredient
{ 
    public int RecipeIngredientId { get; set; }
    
    public int RecipeId { get; set; }
    
    public int IngredientId { get; set; }
    
    public int Quantity { get; set; }
    
    public string Unit { get; set; }
}