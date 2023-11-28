using FluentAssertions;
using infrastructure.Models;
using infrastructure.Repositories;

namespace PlaywrightTests;

public class IngredientTests
{
    private IngredientRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new IngredientRepository();
    }
    
    [Test]
    public async Task ShouldSuccessfullyCreateIngredient()
        {
        Ingredient ingredientToAdd = new Ingredient
        {
            IngredientName = "A Test Ingredient",
        };
        Ingredient addedIngredient = _repository.CreateIngredient(ingredientToAdd);
        
        Ingredient retrievedIngredient = _repository.GetIngredientByName(addedIngredient.IngredientName);
        
        retrievedIngredient.Should().BeEquivalentTo(addedIngredient, "it should be the same");
        _repository.DeleteIngredient(retrievedIngredient.IngredientId);
        Assert.Pass("We did it!");
        
        }
    [Test]
    public async Task ShouldSuccessfullyCreateRecipeIngredient()
    {
        RecipeIngredient recipeIngredientToAdd = new RecipeIngredient
        {
            RecipeId = 15,
            IngredientId = 4,
            Quantity = 5,
            Unit = "Test Unit"
        };
        RecipeIngredient addedRecipeIngredient = _repository.CreateRecipeIngredient(recipeIngredientToAdd);
        
        RecipeIngredient retrievedRecipeIngredient = _repository.GetRecipeIngredientById(addedRecipeIngredient.RecipeIngredientId);
        
        retrievedRecipeIngredient.Should().BeEquivalentTo(addedRecipeIngredient, "it should be the same");
        _repository.DeleteRecipeIngredient(retrievedRecipeIngredient.RecipeIngredientId);
        Assert.Pass("We did it!");
        
    }
    [Test]
    public async Task ShouldSuccessfullyGetAllIngredientsFromRecipe()
    {
        RecipeIngredient recipeIngredientToAdd = new RecipeIngredient
        {
            RecipeId = 15,
            IngredientId = 4,
            Quantity = 5,
            Unit = "Test Unit"
        };
        RecipeIngredient recipeIngredientToAdd2 = new RecipeIngredient
        {
            RecipeId = 15,
            IngredientId = 4,
            Quantity = 5,
            Unit = "Test Unit"
        };
        RecipeIngredient recipeIngredientToAdd3 = new RecipeIngredient
        {
            RecipeId = 15,
            IngredientId = 4,
            Quantity = 5,
            Unit = "Test Unit"
        };
        RecipeIngredient addedRecipeIngredient = _repository.CreateRecipeIngredient(recipeIngredientToAdd);
        RecipeIngredient addedRecipeIngredient2 = _repository.CreateRecipeIngredient(recipeIngredientToAdd2);
        RecipeIngredient addedRecipeIngredient3 = _repository.CreateRecipeIngredient(recipeIngredientToAdd3);
        
        IEnumerable<IngredientsForRecipe> retrievedRecipeIngredients = _repository.GetAllIngredientsFromRecipe(15);
        
        retrievedRecipeIngredients.Should().HaveCount(3);
        _repository.DeleteRecipeIngredient(addedRecipeIngredient.RecipeIngredientId);
        _repository.DeleteRecipeIngredient(addedRecipeIngredient2.RecipeIngredientId);
        _repository.DeleteRecipeIngredient(addedRecipeIngredient3.RecipeIngredientId);
        Assert.Pass("We did it!");
        
    }
    [Test]
    public async Task ShouldSuccessfullyUpdateIngredient()
    {
        Ingredient ingredientToAdd = new Ingredient
        {
            IngredientName = "Test Ingredient2",
        };
        Ingredient addedIngredient = _repository.CreateIngredient(ingredientToAdd);
        
        Ingredient retrievedIngredient = _repository.GetIngredientByName(addedIngredient.IngredientName);
        
        retrievedIngredient.Should().BeEquivalentTo(addedIngredient, "it should be the same");
        
        retrievedIngredient.IngredientName = "Updated Ingredient";
        
        Ingredient updatedIngredient = _repository.UpdateIngredient(retrievedIngredient);
        
        updatedIngredient.Should().BeEquivalentTo(retrievedIngredient, "it should be the same");
        _repository.DeleteIngredient(updatedIngredient.IngredientId);
        Assert.Pass("We did it!");
   
        
    }
    [Test]
    public async Task ShouldSuccessfullyUpdateRecipeIngredient()
    {
        RecipeIngredient recipeIngredientToAdd = new RecipeIngredient
        {
            RecipeId = 15,
            IngredientId = 4,
            Quantity = 5,
            Unit = "Test Unit"
        };
        RecipeIngredient addedRecipeIngredient = _repository.CreateRecipeIngredient(recipeIngredientToAdd);
        
        RecipeIngredient retrievedRecipeIngredient = _repository.GetRecipeIngredientById(addedRecipeIngredient.RecipeIngredientId);
        
        retrievedRecipeIngredient.Should().BeEquivalentTo(addedRecipeIngredient, "it should be the same");
        
        retrievedRecipeIngredient.Quantity = 10;
        
        RecipeIngredient updatedRecipeIngredient = _repository.UpdateRecipeIngredient(retrievedRecipeIngredient);
        
        updatedRecipeIngredient.Should().BeEquivalentTo(retrievedRecipeIngredient, "it should be the same");
        _repository.DeleteRecipeIngredient(updatedRecipeIngredient.RecipeIngredientId);
        Assert.Pass("We did it!");
   
        
    }
   
}