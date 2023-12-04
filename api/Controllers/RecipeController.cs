using infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using service;
using Microsoft.AspNetCore.Mvc;
namespace Recipe_Web_App.Controllers;

public class RecipeController
{
    private readonly RecipeService _service;
    private readonly BlobService _BlobService;
    public RecipeController(RecipeService service, BlobService blobService)
    {
        _service = service;
        _BlobService = blobService;
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
    
    [Route("api/recipes/picture/{id}")]
    [HttpGet]
    public IActionResult UploadRecipePicture([FromRoute] int id, IFormFile? picture)
    {
       String blobURL = _BlobService.Save(picture.OpenReadStream(), id);

       Recipe recipe = _service.GetRecipeById(id);
       recipe.RecipeURL = blobURL;
       _service.UpdateRecipe(recipe);
       return new OkResult();
    }
    
    

}