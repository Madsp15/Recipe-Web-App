using infrastructure.Models;
using infrastructure.Repositories;
using Recipe_Web_App.TransferModels;


namespace service;

public class ReviewService
{
    private readonly ReviewRepository _reviewRepository;

    public ReviewService(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public Review CreateReview(Review review)
    {
        return _reviewRepository.CreateReview(review);
    }

    public Review UpdateReview(Review review)
    {
        return _reviewRepository.UpdateReview(review);
    }

    public bool DeleteReview(int reviewId)
    {
        return _reviewRepository.DeleteReview(reviewId);
    }

    public double GetAverageRating(int recipeId)
    {
        return _reviewRepository.GetAverageRatingForRecipe(recipeId);
    }

    public IEnumerable<ReviewWithUser> GetRecipeReview(int recipeId)
    {
        return _reviewRepository.GetRecipeReview(recipeId);
    }
}