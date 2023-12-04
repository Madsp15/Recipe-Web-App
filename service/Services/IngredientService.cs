using infrastructure.Models;
using infrastructure.Repositories;

namespace service;

public class IngredientService
{
    private readonly IngredientRepository _ingredientRepository;
    
    public IngredientService(IngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }
    
    public Ingredient CreateIngredient(Ingredient ingredient)
    {
        return _ingredientRepository.CreateIngredient(ingredient);
    }
    
    public bool DeleteIngredient(int id)
    {
        return _ingredientRepository.DeleteIngredient(id);
    }
    
    public Ingredient UpdateIngredient(Ingredient ingredient)
    {
        return _ingredientRepository.UpdateIngredient(ingredient);
    }
    
    public Ingredient GetIngredientByName(string name)
    {
        return _ingredientRepository.GetIngredientByName(name);
    }
    public RecipeIngredient CreateRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        return _ingredientRepository.CreateRecipeIngredient(recipeIngredient);
    }
    public bool DeleteRecipeIngredient(int id)
    {
        return _ingredientRepository.DeleteRecipeIngredient(id);
    }
    public RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        return _ingredientRepository.UpdateRecipeIngredient(recipeIngredient);
    }
    public RecipeIngredient GetRecipeIngredientById(int id)
    {
        return _ingredientRepository.GetRecipeIngredientById(id);
    }
    public IEnumerable<IngredientsForRecipe> GetIngredientsForRecipe(int id)
    {
        return _ingredientRepository.GetAllIngredientsFromRecipe(id);
    }
    public RecipeIngredient GetRecipeIngredientByIngredientId(int id)
    {
        return _ingredientRepository.GetRecipeIngredientById(id);
    }
    
}