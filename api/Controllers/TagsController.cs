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
    
    [Route("api/tags")]
    [HttpPost]
    public Tag CreateTag([FromBody] Tag tag)
    {
        return _tagsService.CheckTag(tag);
    }
    
    [Route("api/tags/{id}")]
    [HttpDelete]
    public IActionResult DeleteTag([FromRoute] int id)
    {
        if (GetTagById(id) == null)
            return NotFound("Tag not found");
            
        if (_tagsService.DeleteTag(id))
            return Ok("Tag deleted");
       
        return BadRequest("Tag not deleted");
       
    }
    
    [Route("api/tags")]
    [HttpPut]
    public Tag UpdateTag([FromBody] Tag tag)
    {
        return _tagsService.UpdateTag(tag);
    }
    
    [Route("api/tagname")]
    [HttpGet]
    public Tag GetTagByName([FromQuery] string name)
    {
        return _tagsService.GetTagByName(name);
    }
    
    [Route("api/tags")]
    [HttpGet]
    public List<Tag> GetAllTags()
    {
        return _tagsService.GetAllTags();
    }
    
    [Route("api/tags/recipe/{recipeId}")]
    [HttpGet]
    public List<Tag> GetTagsByRecipeId([FromRoute] int recipeId)
    {
        return _tagsService.GetTagsByRecipeId(recipeId);
    }
    
    [Route("api/tags/{id}")]
    [HttpGet]
    public Tag GetTagById([FromRoute] int id)
    {
        return _tagsService.GetTagById(id);
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