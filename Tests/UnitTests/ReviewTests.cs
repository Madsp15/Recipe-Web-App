using FluentAssertions;
using infrastructure.Models;
using infrastructure.Repositories;

namespace PlaywrightTests;

public class ReviewTests
{
    private ReviewRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        _repository = new ReviewRepository();
    }
    
    [Test]
    public async Task ShouldSuccessfullyCreateReview()
    {
        Review reviewToAdd = new Review
        {
            RecipeId = 15,
            UserId = 2,
            Rating = 5,
            Comment = "Test Comment",
            DateRated = "Test Date"
            
        };
        Review addedReview = _repository.CreateReview(reviewToAdd);
        
        
        reviewToAdd.Comment.Should().BeEquivalentTo(addedReview.Comment, "it should be the same");
        _repository.DeleteReviewFromRecipe(15);
        Assert.Pass("We did it!");
    }
    
    [Test]
    public async Task ShouldSuccessfullyGetAverageRating()
    {
        Review reviewToAdd = new Review
        {
            RecipeId = 15,
            UserId = 2,
            Rating = 3,
            Comment = "Test Comment",
            DateRated = "Test Date"
            
        };
        Review reviewToAdd2 = new Review
        {
            RecipeId = 15,
            UserId = 2,
            Rating = 5,
            Comment = "Test Comment",
            DateRated = "Test Date"
            
        };
        Review reviewToAdd3 = new Review
        {
            RecipeId = 15,
            UserId = 2,
            Rating = 1,
            Comment = "Test Comment",
            DateRated = "Test Date"
            
        };
        
         Review review1 = _repository.CreateReview(reviewToAdd);
         Review review2 = _repository.CreateReview(reviewToAdd2);
         Review review3 =  _repository.CreateReview(reviewToAdd3);
         
        _repository.GetAverageRatingForRecipe(15).Should().Be(3);

        _repository.DeleteReviewFromRecipe(15);
        Assert.Pass("We did it!");
    }
}