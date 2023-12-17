using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App.TransferModels;
using service;

namespace Recipe_Web_App.Controllers;

public class ReviewController: ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [Route("/api/reviews")]
    [HttpPost]
    public Review CreateReview([FromBody]Review review)
    {
        return _reviewService.CreateReview(review);
    }

    [Route("/api/{reviewId}")]
    [HttpPut]
    public Review UpdateReview([FromBody]Review review, [FromRoute] int reviewId)
    {
        review.ReviewId = reviewId;
        return _reviewService.UpdateReview(review);
    }

    [Route("/api/reviews/{reviewId}")]
    [HttpDelete]
    public IActionResult DeleteReview([FromRoute] int reviewId)
    {
        if(_reviewService.DeleteReview(reviewId) == null)
        {
            return NotFound("Review not found");
        }
        return _reviewService.DeleteReview(reviewId) ? Ok() : Problem();
    }

    [Route("api/recipe/averagerating/{recipeId}")]
    [HttpGet]
    public double? GetAverageRating([FromRoute]int recipeId)
    {
        return _reviewService.GetAverageRating(recipeId);
    }
    
    [Route("api/reviews/{recipeId}")]
    [HttpGet]
    public IEnumerable<ReviewWithUser> GetRecipeReview([FromRoute]int recipeId)
    {
        return _reviewService.GetRecipeReview(recipeId);
    }
}