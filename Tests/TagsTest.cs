using FluentAssertions;
using infrastructure;
using infrastructure.Models;

namespace PlaywrightTests;

public class TagsTest
{
    private TagsRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new TagsRepository();
    }
    
    [Test]
    public async Task ShouldSuccessfullyCreateTag()
    {
        Tag tagToAdd = new Tag
        {
            tagName = "Test Tag",
        };
        Tag addedTag = _repository.CreateTag(tagToAdd);
        Tag retrievedTag = _repository.GetTagById(addedTag.tagId);
        
        retrievedTag.Should().BeEquivalentTo(addedTag, "it should be the same");
        _repository.DeleteTag(retrievedTag.tagId);
        Assert.Pass("We did it!");
        
    }
    
    [Test]
    public async Task ShouldSuccessfullyUpdateTag()
    {
        Tag tagToAdd = new Tag
        {
            tagName = "Test Tag",
        };
        Tag addedTag = _repository.CreateTag(tagToAdd);
        addedTag.tagName = "Updated Test Tag";
        Tag updatedTag = _repository.UpdateTag(addedTag);
        updatedTag.Should().BeEquivalentTo(addedTag, "it should be the same");
        _repository.DeleteTag(updatedTag.tagId);
        Assert.Pass("We did it!");
    }
    
    
}