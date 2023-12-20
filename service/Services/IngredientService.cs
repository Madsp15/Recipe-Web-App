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
    
    public Ingredient GetIngredientByName(string name)
    {
        return _ingredientRepository.GetIngredientByName(name);
    }
    public RecipeIngredient CreateRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        return _ingredientRepository.CreateRecipeIngredient(recipeIngredient);
    }
    public bool DeleteRecipeIngredient(int ingredientId, int recipeId)
    {
        return _ingredientRepository.DeleteRecipeIngredient(ingredientId, recipeId);
    }
    public IEnumerable<Ingredients> GetIngredientsForRecipe(int id)
    {
        return _ingredientRepository.GetAllIngredientsFromRecipe(id);
    }

    public void AddOneIngredientToRecipe(Ingredients ingredient, int recipeId)
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
    public bool DeleteIngredientsByName(string tagname, int recipeId)
    {
        IEnumerable<Ingredient> ingredients = _ingredientRepository.GetAllIngredients();
        foreach (Ingredient  ingredient in ingredients)
        {
            if (tagname.Equals(ingredient.IngredientName))
            {
               return _ingredientRepository.DeleteIngredientsFromRecipeByName(ingredient.IngredientId, recipeId);
            }
        }

        return false;
    }
}