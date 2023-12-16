using Dapper;
using infrastructure.Models;
using Recipe_Web_App.TransferModels;

namespace infrastructure.Repositories;

public class ReviewRepository
{
    String date = DateHelper.GetDate();
    public Review CreateReview(Review review)
    {
        review.DateRated = date;
        var sql = $@"INSERT INTO reviews(recipeId, userId, rating, comment, dateRated)
                        VALUES(@recipeId, @userId, @rating, @comment, @dateRated)
                        RETURNING
                        reviewId as {nameof(Review.ReviewId)},
                        recipeId as {nameof(Review.RecipeId)},
                        userId as {nameof(Review.UserId)},
                        rating as {nameof(Review.Rating)},
                        comment as {nameof(Review.Comment)},
                        dateRated as {nameof(Review.DateRated)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Review>(sql, new
            {
                recipeId = review.RecipeId,
                userId = review.UserId,
                rating = review.Rating,
                comment = review.Comment,
                dateRated = review.DateRated

            });
        }
    }
    
    public Review UpdateReview(Review review)
    {
        review.DateRated = date;
        var sql = $@"UPDATE reviews
                 SET rating = @rating,
                     comment = @comment,
                     dateRated = @dateRated
                 WHERE reviewId = @reviewId
                 RETURNING
                    reviewId as {nameof(Review.ReviewId)},
                    recipeId as {nameof(Review.RecipeId)},
                    userId as {nameof(Review.UserId)},
                    rating as {nameof(Review.Rating)},
                    comment as {nameof(Review.Comment)},
                    dateRated as {nameof(Review.DateRated)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Review>(sql, new
            {
                reviewId = review.ReviewId,
                rating = review.Rating,
                comment = review.Comment,
                dateRated = review.DateRated
            });
        }
    }
    
    public bool DeleteReview(int reviewId)
    {
        var sql = $@"DELETE FROM reviews WHERE reviewId = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id = reviewId }) == 1;
        }
    }
    
    public double GetAverageRatingForRecipe(int recipeId)
    {
        var sql = $@"SELECT AVG(rating) 
                 FROM reviews
                 WHERE recipeId = @recipeId";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<double>(sql, new { recipeId });
        }
    }
    public Review GetReviewById(int reviewId)
    {
        var sql = $@"SELECT * FROM reviews WHERE reviewId = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Review>(sql, new { id = reviewId });
        }
    }

    public IEnumerable<ReviewWithUser> GetRecipeReview(int recipeId)
    {
        var sql = $@"SELECT r.*, u.userId, u.username, u.userAvatarUrl
        FROM reviews r
        JOIN users u ON r.userId = u.userId
        WHERE r.recipeId = @id
";
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<ReviewWithUser>(sql, new { id = recipeId });
        }
    }
}