using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Recipe_Web_App.Controllers;

public class IngredientsController : ControllerBase
{ 
    private readonly IngredientService _service;
    
    public IngredientsController(IngredientService service)
    {
        _service = service;
    }
    

    [Route("api/add/ingredient/recipe/{recipeId}")]
    [HttpPost]
    public void AddIngredientToRecipe([FromBody]Ingredients ingredient, [FromRoute]int recipeId)
    {
        _service.AddOneIngredientToRecipe(ingredient, recipeId);
    }
    
    [Route("api/recipeingredients")]
    [HttpPost]
    public RecipeIngredient CreateRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
    {
        return _service.CreateRecipeIngredient(recipeIngredient);
    }
    
    [Route("api/delete/recipeingredients/{ingredientId}/{recipeId}")]
    [HttpDelete]
    public bool DeleteRecipeIngredient([FromRoute] int ingredientId, int recipeId)
    {
        return _service.DeleteRecipeIngredient(ingredientId, recipeId);
    }
    
    [Route("api/recipeingredients/recipe/{id}")]
    [HttpGet]
    public IEnumerable<Ingredients> GetIngredientsForRecipe([FromRoute] int id)
    {
        return _service.GetIngredientsForRecipe(id);
    }

    [Route("api/recipeingredients/recipe/{id}/{tagname}")]
    [HttpDelete]
    public bool DeleteRecipeIngredientsByName(string tagname, int id)
    {
        return _service.DeleteIngredientsByName(tagname, id);
    }
}