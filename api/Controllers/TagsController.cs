using infrastructure;
using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using service;

namespace Recipe_Web_App.Controllers;

public class TagsController: ControllerBase
{
    private readonly TagsService _tagsService;

    public TagsController(TagsService tagsService)
    {
        _tagsService = tagsService;
    }

    [Route("api/add/tag/{recipeId}")]
    [HttpPost]
    public void AddTagToRecipe([FromBody]string tagname, [FromRoute] int recipeId)
    {
        _tagsService.AddTagToRecipe(_tagsService.GetTagId(tagname), recipeId);
    }
    
    
    [Route("api/tags/recipe/{recipeId}")]
    [HttpGet]
    public List<Tag> GetTagsByRecipeId([FromRoute] int recipeId)
    {
        return _tagsService.GetTagsByRecipeId(recipeId);
    }
    
    
    [Route("api/tagnames/{recipeId}")]
    [HttpGet]
    public List<string> GetTagNamesByRecipeId([FromRoute] int recipeId)
    {
        return _tagsService.GetTagNamesByRecipeId(recipeId);
    }

    [Route("api/tag/{tagname}/recipe/{recipeId}")]
    [HttpDelete]
    public bool DeleteTagByName(string tagname, int recipeId)
    {
        return _tagsService.DeleteTagByName(tagname, recipeId);
    }
}