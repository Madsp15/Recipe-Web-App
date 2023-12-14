using FluentAssertions;
using infrastructure;
using infrastructure.Repositories;

namespace PlaywrightTests;

public class RecipeTests
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
            Instructions = "Test Instructions",
            Duration = "Test Duration",
            Servings = 2,
            RecipeURL = "Test Recipe URL",
                
        };
        Recipe addedRecipe = _repository.CreateRecipe(recipeToAdd);
        
        Recipe retrievedRecipe = _repository.GetRecipeById(addedRecipe.RecipeId);
        retrievedRecipe.Should().BeEquivalentTo(addedRecipe, "it should be the same");
        _repository.DeleteRecipe(retrievedRecipe.RecipeId);
        Assert.Pass("We did it!");
    }
    [Test]
    public async Task ShouldSuccessfullyUpdateRecipe()
    {
        Recipe recipeToAdd = new Recipe
        {
            Title = "Test Recipe",
            Description = "Test Description",
            UserId = 2,
            Instructions = "Test Instructions",
            Duration = "Test Duration",
            Servings = 2,
            RecipeURL = "Test Recipe URL",
                
        };
        Recipe addedRecipe = _repository.CreateRecipe(recipeToAdd);
        addedRecipe.Title = "Updated Test Recipe";
        Recipe updatedRecipe = _repository.updateRecipe(addedRecipe);
        updatedRecipe.Should().BeEquivalentTo(addedRecipe, "it should be the same");
        _repository.DeleteRecipe(updatedRecipe.RecipeId);
        Assert.Pass("We did it!");
    }
    
}