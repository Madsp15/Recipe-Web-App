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
            TagName = "Test Tag",
        };
        Tag addedTag = _repository.CreateTag(tagToAdd);
        Tag retrievedTag = _repository.GetTagByName(addedTag.TagName);
        
        retrievedTag.TagName.Should().BeEquivalentTo(addedTag.TagName, "it should be the same");
        Assert.Pass("We did it!");
    }
}