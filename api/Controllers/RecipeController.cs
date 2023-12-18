using infrastructure;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App.TransferModels;
using service;


namespace Recipe_Web_App.Controllers;

public class RecipeController : ControllerBase
{
    private readonly RecipeService _service;
    private readonly BlobService _BlobService;
    private readonly TagsService _tagService;
    private readonly IngredientService _ingredientService;
    public RecipeController(RecipeService service, BlobService blobService, TagsService tagsService, IngredientService ingredientService)
    {
        _service = service;
        _BlobService = blobService;
        _tagService = tagsService;
        _ingredientService = ingredientService;
    }

    [Route("api/recipes")]
    [HttpPost]
    public Recipe CreateRecipe([FromBody] RecipeDto recipeDto)
    {
        Recipe createdRecipe = new Recipe
        {
            Title = recipeDto.Title,
            UserId = recipeDto.UserId,
            Instructions = recipeDto.Instructions,
            RecipeURL = recipeDto.RecipeURL,
            Description = recipeDto.Description,
            Servings = recipeDto.Servings,
            Duration = recipeDto.Duration
        };
        var recipe = _service.CreateRecipe(createdRecipe);
        
        _tagService.AddTagsToRecipe(recipe.RecipeId, _tagService.GetTagIds(recipeDto.SelectedTags));
        Console.WriteLine("Recipe ingredients: "+recipeDto.Ingredients);
        _ingredientService.AddIngredientsToRecipe(recipeDto.Ingredients, recipe.RecipeId);

        return recipe;
    }
    
    [Route("api/recipes/{id}")]
    [HttpDelete]
    public bool DeleteRecipe(int id)
    {
        return _service.DeleteRecipe(id);
    }

    [Route("api/recipes")]
    [HttpPut]
    public Recipe UpdateRecipe([FromBody] Recipe recipe)
    {
        return _service.UpdateRecipe(recipe);
    }
    
    [Route("api/recipes")]
    [HttpGet]
    public IEnumerable<Recipe> GetAllRecipes()
    {
        return _service.GetAllRecipes();
    }
    
    [Route("api/random/recipes")]
    [HttpGet]
    public IEnumerable<Recipe> GetRandomRecipes()
    {
        return _service.GetRandomRecipes();
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
    [HttpPut]
    public IActionResult UploadRecipePicture([FromRoute] int id, IFormFile? picture)
    {
        if (picture == null)
        {
            return BadRequest("No file was uploaded");
        }
        Recipe recipe = _service.GetRecipeById(id);
        
        if (recipe == null)
        {
            return NotFound("Recipe not found");
        } 
        string blobUrl = _BlobService.Save(picture.OpenReadStream(), id); 
        recipe.RecipeURL = blobUrl;
        _service.UpdateRecipe(recipe);
        return Ok();
    }
    
    
    

}