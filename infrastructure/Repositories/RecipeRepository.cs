using Dapper;

namespace infrastructure.Repositories;

public class RecipeRepository
{
    String date = DateHelper.GetDate();
    public Recipe CreateRecipe(Recipe recipe)
    {
        //instructions needs to formatted correctly with line breaks
    recipe.DateCreated = date;
        var sql = $@"INSERT INTO recipes(userid, title, description, instructions, recipeurl, datecreated, duration, servings)
                        VALUES(@userId, @title, @description, @instructions, @recipeURL, @dateCreated, @duration, @servings)
                        RETURNING
                        RecipeId as {nameof(Recipe.RecipeId)},
                        UserId as {nameof(Recipe.UserId)},
                        Title as {nameof(Recipe.Title)},
                        Description as {nameof(Recipe.Description)},
                        instructions as {nameof(Recipe.Instructions)},
                        RecipeURL as {nameof(Recipe.RecipeURL)},
                        DateCreated as {nameof(Recipe.DateCreated)},
                        Duration as {nameof(Recipe.Duration)},
                        Servings as {nameof(Recipe.Servings)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Recipe>(sql, new
            {
                recipeId = recipe.RecipeId,
                userId = recipe.UserId,
                title = recipe.Title,
                description = recipe.Description,
                instructions = recipe.Instructions,
                recipeURL = recipe.RecipeURL,
                dateCreated = recipe.DateCreated,
                duration = recipe.Duration,
                servings = recipe.Servings
            });
        }
    }
    public bool DeleteRecipe(int id)
    {
        var sql = $@"DELETE FROM recipes WHERE recipeId = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id }) == 1;
        }
    }
    
    public IEnumerable<Recipe> GetAllRecipes()
    {
        var sql = $@"SELECT * FROM recipes
                        ORDER BY title;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Recipe>(sql);
        }
    }
    
    public IEnumerable<Recipe> GetRandomRecipes()
    {
        var sql = $@"SELECT * FROM recipes ORDER BY RANDOM() LIMIT 9;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Recipe>(sql);
        }
    }
    
 public Recipe UpdateRecipe(Recipe recipe)
     {
         recipe.DateCreated = date;
          using (var conn = DataConnection.DataSource.OpenConnection())
          {
                var sql = $@"UPDATE recipes
                            SET title = @title,
                                description = @description,
                                instructions = @instructions,
                                recipeurl = @recipeURL,
                                datecreated = @dateCreated,
                                duration = @duration,
                                servings = @servings
                            WHERE recipeId = @id
                            RETURNING
                            RecipeId as {nameof(Recipe.RecipeId)},
                            UserId as {nameof(Recipe.UserId)},
                            Title as {nameof(Recipe.Title)},
                            Description as {nameof(Recipe.Description)},
                            instructions as {nameof(Recipe.Instructions)},
                            RecipeURL as {nameof(Recipe.RecipeURL)},
                            DateCreated as {nameof(Recipe.DateCreated)},
                            Duration as {nameof(Recipe.Duration)},
                            Servings as {nameof(Recipe.Servings)};";
                return conn.QueryFirst<Recipe>(sql, new
                {
                    id = recipe.RecipeId,
                    title = recipe.Title,
                    description = recipe.Description,
                    instructions = recipe.Instructions,
                    recipeURL = recipe.RecipeURL,
                    dateCreated = recipe.DateCreated,
                    duration = recipe.Duration,
                    servings = recipe.Servings
                });
          }
     }
    public IEnumerable<Recipe> GetRecipeByUserId(int userId)
    {
        
        var sql = $@"SELECT * FROM recipes WHERE userId = @userId;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Recipe>(sql, new { userId });
        }
        
    }

    public Recipe GetRecipeById(int recipeId)
    {
      //since intructions is TEXT in the database, we needed to convert the data type to string by getting it as a reader
        var sql = $@"SELECT recipeid, userid, title, description, CAST(instructions AS TEXT) AS instructions, recipeurl, datecreated, duration, servings
                FROM recipes
                WHERE recipeId = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            var reader = conn.ExecuteReader(sql, new { id = recipeId });

            if (reader.Read())
            {
                var recipe = new Recipe
                {
                    RecipeId = reader.GetInt32(reader.GetOrdinal("recipeid")),
                    UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                    Title = reader.GetString(reader.GetOrdinal("title")),
                    Description = reader.GetString(reader.GetOrdinal("description")),
                    Instructions  = reader.GetString(reader.GetOrdinal("instructions")),
                    RecipeURL = reader.GetString(reader.GetOrdinal("recipeurl")),
                    DateCreated = reader.GetString(reader.GetOrdinal("datecreated")),
                    Duration = reader.GetString(reader.GetOrdinal("duration")),
                    Servings = reader.GetInt32(reader.GetOrdinal("servings"))
                };

                return recipe;
            }
            else
            {
                return null;
            }
        }
    }

    public Recipe GetTrendingRecipe()
    {
        var sql = "SELECT r.* FROM recipes r JOIN (SELECT recipeid, COUNT(reviewid) AS num_reviews, " +
                  "AVG(rating) AS average_rating FROM reviews GROUP BY recipeid ORDER BY num_reviews DESC, " +
                  "average_rating DESC LIMIT 1) top_recipe ON r.recipeid = top_recipe.recipeid";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Recipe>(sql).FirstOrDefault();
        }
    }
}