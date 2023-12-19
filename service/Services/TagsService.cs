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

    public int GetTagId(string tag)
    {
        int tagId;
        Tag existingTags = GetTagByName(tag);
            if (existingTags != null)
            {
                tagId = existingTags.TagId;
            }

            else
            {
                Tag newTag = CreateTag(new Tag { TagName = tag });
                tagId = newTag.TagId;
            }

            return tagId;
    }
    

    public Tag CreateTag(Tag tags)
    {
        return _repository.CreateTag(tags);
    }
    
    public Tag GetTagByName(string name)
    {
        return _repository.GetTagByName(name);
    }
    
    public List<Tag> GetTagsByRecipeId(int recipeId)
    {
        return _repository.GetTagsByRecipeId(recipeId);
    }
    
    public bool AddTagsToRecipe(int recipeId, List<int> tagIds)
    {
        return _repository.AddTagsToRecipe(recipeId, tagIds);
    }

    public bool AddTagToRecipe(int tagId, int recipeId)
    {
        return _repository.AddTagToRecipe(tagId, recipeId);
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