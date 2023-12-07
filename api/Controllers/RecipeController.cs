﻿using infrastructure;
using Microsoft.AspNetCore.Mvc;
using service;


namespace Recipe_Web_App.Controllers;

public class RecipeController : ControllerBase
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
    [HttpPut]
    public IActionResult UploadRecipePicture([FromRoute] int id, IFormFile? picture)
    {
        if (picture == null)
        {
            return BadRequest("No file was uploaded, when you needed the avatar the most he disappeared");
        }
        Recipe recipe = _service.GetRecipeById(id);
        
        if (recipe == null)
        {
            return NotFound("User not found");
        } String blobUrl = _BlobService.Save(picture.OpenReadStream(), id); recipe.RecipeURL = blobUrl; _service.UpdateRecipe(recipe);
        return Ok();
    }
    
    
    

}