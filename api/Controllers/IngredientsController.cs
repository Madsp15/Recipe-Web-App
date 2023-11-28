using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Recipe_Web_App.Controllers;

public class IngredientsController
{ 
    private readonly IngredientService _service;
    
    public IngredientsController(IngredientService service)
    {
        _service = service;
    }
    
    [Route("api/ingredients")]
    [HttpPost]
    public Ingredient CreateIngredient([FromBody] Ingredient ingredient)
    {
        return _service.CreateIngredient(ingredient);
    }
    
    [Route("api/ingredients/{id}")]
    [HttpDelete]
    public bool DeleteIngredient([FromRoute] int id)
    {
        return _service.DeleteIngredient(id);
    }
    
    [Route("api/ingredients")]
    [HttpPut]
    public Ingredient UpdateIngredient([FromBody] Ingredient ingredient)
    {
        return _service.UpdateIngredient(ingredient);
    }
    
    [Route("api/ingredients/{name}")]
    [HttpGet]
    public Ingredient GetIngredientByName([FromRoute] string name)
    {
        return _service.GetIngredientByName(name);
    }
    
    [Route("api/recipeingredients")]
    [HttpPost]
    public RecipeIngredient CreateRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
    {
        return _service.CreateRecipeIngredient(recipeIngredient);
    }
    
    [Route("api/recipeingredients/{id}")]
    [HttpDelete]
    public bool DeleteRecipeIngredient([FromRoute] int id)
    {
        return _service.DeleteRecipeIngredient(id);
    }
    
    [Route("api/recipeingredients")]
    [HttpPut]
    public RecipeIngredient UpdateRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
    {
        return _service.UpdateRecipeIngredient(recipeIngredient);
    }
    
    [Route("api/recipeingredients/{id}")]
    [HttpGet]
    public RecipeIngredient GetRecipeIngredientById([FromRoute] int id)
    {
        return _service.GetRecipeIngredientById(id);
    }
    
    [Route("api/recipeingredients/recipe/{id}")]
    [HttpGet]
    public IEnumerable<IngredientsForRecipe> GetIngredientsForRecipe([FromRoute] int id)
    {
        return _service.GetIngredientsForRecipe(id);
    }
    
    
}