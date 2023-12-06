using infrastructure;
using infrastructure.Models;

namespace service;

public class TagsService
{
    private readonly TagsRepository _repository;

    public TagsService(TagsRepository repository)
    {
        _repository = repository;
    }
    public Tag CheckTag(Tag tag)
    {
        if (CheckIfTagExists(tag.tagName))
        {
            return GetTagByName(tag.tagName);
        }
        return CreateTag(tag);
        
    }
    public Tag CreateTag(Tag tag)
    {
        return _repository.CreateTag(tag);
    }
    public bool DeleteTag(int id)
    {
        return _repository.DeleteTag(id);
    }
    public Tag UpdateTag(Tag tag)
    {
        return _repository.UpdateTag(tag);
    }
    public Tag GetTagByName(string name)
    {
        return _repository.GetTagByName(name);
    }
    public List<Tag> GetAllTags()
    {
        return _repository.GetAllTags();
    }
    
    public List<Tag> GetTagsByRecipeId(int recipeId)
    {
        return _repository.GetTagsByRecipeId(recipeId);
    }
    
    public bool AddTagToRecipe(int recipeId, int tagId)
    {
        return _repository.AddTagToRecipe(recipeId, tagId);
    }
    public Tag GetTagById(int id)
    {
        return _repository.GetTagById(id);
    }
    
    public bool DeleteTagFromRecipe(int recipeId, int tagId)
    {
        return _repository.DeleteTagFromRecipe(recipeId, tagId);
    }
    
    public bool CheckIfTagExists(string name)
    {
        return _repository.CheckIfTagExists(name);
    }
}