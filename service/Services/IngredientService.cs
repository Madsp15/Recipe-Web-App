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
    public IEnumerable<Ingredients> GetIngredientsForRecipe(int id)
    {
        return _ingredientRepository.GetAllIngredientsFromRecipe(id);
    }

    public void AddIngredientsToRecipe(List<Ingredients> ingredients, int recipeId)
    {
        foreach (Ingredients ingredient in ingredients)
        {
            Ingredient existingIngredient = GetIngredientByName(ingredient.IngredientName);

            if (existingIngredient != null)
            {
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    IngredientId = existingIngredient.IngredientId,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit,
                    RecipeId = recipeId
                };

                _ingredientRepository.CreateRecipeIngredient(recipeIngredient);
            }
            else
            {
                Ingredient newIngredient = CreateIngredient(new Ingredient { IngredientName = ingredient.IngredientName });
                
                RecipeIngredient recipeIngredient = new RecipeIngredient()
                {
                    IngredientId = newIngredient.IngredientId,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit,
                    RecipeId = recipeId
                };
                _ingredientRepository.CreateRecipeIngredient(recipeIngredient);
            }
        }
    }
}