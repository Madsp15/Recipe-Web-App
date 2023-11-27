using FluentAssertions;
using infrastructure;

namespace PlaywrightTests;

public class CRUDReviewTest
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
        
         _repository.CreateReview(reviewToAdd);
         _repository.CreateReview(reviewToAdd2);
         _repository.CreateReview(reviewToAdd3);
         
        _repository.GetAverageRatingForRecipe(15).Should().Be(6);
        Assert.Pass("We did it!");
        _repository.DeleteReview(reviewToAdd.ReviewId);
        _repository.DeleteReview(reviewToAdd2.ReviewId);
        _repository.DeleteReview(reviewToAdd3.ReviewId);
    }
}