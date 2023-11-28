using infrastructure;
using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
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

    [Route("/api/{reviewId}")]
    [HttpDelete]
    public bool DeleteReview([FromRoute] int reviewId)
    {
        return _reviewService.DeleteReview(reviewId);
    }

    [Route("api/recipes/{recipeId}/averagerating")]
    [HttpGet]
    public double GetAverageRating([FromRoute]int recipeId)
    {
        return _reviewService.GetAverageRating(recipeId);
    }
}