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
        if (CheckIfTagExists(tag.TagName))
        {
            return GetTagByName(tag.TagName);
        }
        else return CreateTag(tag);
    }

    public List<int> GetTagIds(List<string> tags)
    {
        List<int> tagIds = new List<int>();

        foreach (string tag in tags)
        {
            Tag existingTag = GetTagByName(tag);
            if (existingTag != null)
            {
                tagIds.Add(existingTag.TagId);
            }

            else
            {
                Tag newTag = CreateTag(new Tag { TagName = tag });
                tagIds.Add(newTag.TagId);
            }
        }

        return tagIds;
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
}