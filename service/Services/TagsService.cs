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
    public Tag CheckTag(Tag tags)
    {
        if (CheckIfTagExists(tags.TagName))
        {
            return GetTagByName(tags.TagName);
        }
        else return CreateTag(tags);
    }

    public List<int> GetTagIds(List<string> tags)
    {
        List<int> tagIds = new List<int>();

        foreach (string tag in tags)
        {
            Tag existingTags = GetTagByName(tag);
            if (existingTags != null)
            {
                tagIds.Add(existingTags.TagId);
            }

            else
            {
                Tag newTags = CreateTag(new Tag { TagName = tag });
                tagIds.Add(newTags.TagId);
            }
        }

        return tagIds;
    }

    public Tag CreateTag(Tag tags)
    {
        return _repository.CreateTag(tags);
    }
    public bool DeleteTag(int id)
    {
        return _repository.DeleteTag(id);
    }
    public Tag UpdateTag(Tag tags)
    {
        return _repository.UpdateTag(tags);
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
    
    public bool AddTagsToRecipe(int recipeId, List<int> tagIds)
    {
        return _repository.AddTagsToRecipe(recipeId, tagIds);
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
    
    public List<string> GetTagNamesByRecipeId(int recipeId)
    {
        return _repository.GetTagNamesByRecipeId(recipeId);
    }

    public bool DeleteTagByName(string tagname, int recipeId)
    {
        List<Tag> tags = _repository.GetAllTags();
        foreach (Tag tag in tags)
        {
            if (tagname.Equals(tag.TagName))
            {
                return _repository.DeleteTagByName(tag.TagId, recipeId);
            }
        }

        return false;
    }
}