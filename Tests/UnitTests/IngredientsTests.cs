using FluentAssertions;
using infrastructure.Models;
using infrastructure.Repositories;
namespace PlaywrightTests;

public class IngredientsTests
{
     private IngredientRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new IngredientRepository();
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
        _repository.CreateRecipeIngredient(recipeIngredientToAdd);
        _repository.CreateRecipeIngredient(recipeIngredientToAdd2);
        _repository.CreateRecipeIngredient(recipeIngredientToAdd3);
        
        IEnumerable<Ingredients> retrievedRecipeIngredients = _repository.GetAllIngredientsFromRecipe(15);
        
        retrievedRecipeIngredients.Should().HaveCount(3);
        _repository.DeleteRecipeIngredient(15);
    }
}
