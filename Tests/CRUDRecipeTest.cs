using FluentAssertions;
using infrastructure;

namespace PlaywrightTests;

public class CRUDRecipeTest
{
    private RecipeRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new RecipeRepository();
    }
    
    [Test]
    public async Task ShouldSuccessfullyCreateRecipe()
    {
        Recipe recipeToAdd = new Recipe
        {
            Title = "Test Recipe",
            Description = "Test Description",
            UserId = 2,
            Instuctions = "Test Instructions",
            Notes = "Test Notes",
            RecipeURL = "Test Recipe URL",
                
        };
        Recipe addedRecipe = _repository.CreateRecipe(recipeToAdd);
        
        Recipe retrievedRecipe = _repository.GetRecipeById(addedRecipe.RecipeId);
        retrievedRecipe.Should().BeEquivalentTo(addedRecipe, "it should be the same");
        _repository.DeleteRecipe(retrievedRecipe.RecipeId);
        Assert.Pass("We did it!");
    }
}