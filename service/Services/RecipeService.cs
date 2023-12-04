using infrastructure;
using infrastructure.Repositories;

namespace service;

public class RecipeService
{
    private readonly RecipeRepository _repository;

    public RecipeService(RecipeRepository repository)
    {
        _repository = repository;
    }
    
    public Recipe CreateRecipe(Recipe recipe)
    {
        return _repository.CreateRecipe(recipe);
    }
    
    public bool DeleteRecipe(int id)
    {
        return _repository.DeleteRecipe(id);
    }
    
    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _repository.GetAllRecipes();
    }
    
    public Recipe UpdateRecipe(Recipe recipe)
    {
        return _repository.updateRecipe(recipe);
    }
    
    public IEnumerable<Recipe> GetRecipeByUserId(int id)
    {
        return _repository.GetRecipeByUserId(id);
    }
    public Recipe GetRecipeById(int id)
    {
        return _repository.GetRecipeById(id);
    }
}