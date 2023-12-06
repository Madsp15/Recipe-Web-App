using FluentAssertions;
using infrastructure;
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
        
        Review retrievedReview = _repository.GetReviewById(addedReview.ReviewId);
        
        retrievedReview.Should().BeEquivalentTo(addedReview, "it should be the same");
        _repository.DeleteReview(retrievedReview.ReviewId);
        Assert.Pass("We did it!");
        
    }
    [Test]
    public async Task ShouldSuccessfullyGetAverageRating()
    {
        Review reviewToAdd = new Review
        {
            RecipeId = 15,
            UserId = 2,
            Rating = 12,
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
         
        _repository.GetAverageRatingForRecipe(15).Should().Be(6);
        
        _repository.DeleteReview(review1.ReviewId);
        _repository.DeleteReview(review2.ReviewId);
        _repository.DeleteReview(review3.ReviewId);
        Assert.Pass("We did it!");
    }
    
    [Test]
    public async Task ShouldSuccessfullyUpdateReview()
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
        addedReview.Comment = "Updated Test Comment";
        Review updatedReview = _repository.UpdateReview(addedReview);
        updatedReview.Should().BeEquivalentTo(addedReview, "it should be the same");
        _repository.DeleteReview(updatedReview.ReviewId);
        Assert.Pass("We did it!");
        
    }
}