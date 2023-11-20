using infrastructure;
using service;
using Microsoft.AspNetCore.Mvc;
namespace Recipe_Web_App.Controllers;

public class RecipeController
{
    private readonly RecipeService _service;

    public RecipeController(RecipeService service)
    {
        _service = service;
    }

    [Route("api/recipes")]
    [HttpPost]
    public Recipe CreateRecipe([FromBody] Recipe recipe)
    {
        return _service.CreateRecipe(recipe);
    }
    [Route("api/recipes/{id}")]
    [HttpDelete]
    public bool DeleteRecipe(int id)
    {
        return _service.DeleteRecipe(id);
    }

    [Route("api/recipes")]
    [HttpPut]
    public Recipe updateRecipe([FromBody] Recipe recipe)
    {
        return _service.UpdateRecipe(recipe);
    }
    
    [Route("api/recipes")]
    [HttpGet]
    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _service.GetAllRecipes();
    }
    
    [Route("api/recipes/user{id}")]
    [HttpGet]
    public IEnumerable<Recipe> GetRecipeByUserId([FromRoute]int id)
    {
        return _service.GetRecipeByUserId(id);
    }
    [Route("api/recipes/{id}")]
    [HttpGet]
    public Recipe GetRecipeById([FromRoute]int id)
    {
        return _service.GetRecipeById(id);
    }
    
    

}