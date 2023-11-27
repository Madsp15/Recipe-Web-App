using Dapper;

namespace infrastructure;

public class RecipeRepository
{
    String date = DateHelper.GetDate();
    public Recipe CreateRecipe(Recipe recipe)
    {
    recipe.DateCreated = date;
        var sql = $@"INSERT INTO recipes(userid, title, description, instructions, recipeurl, datecreated, notes)
                        VALUES(@userId, @title, @description, @instructions, @recipeURL, @dateCreated, @notes)
                        RETURNING
                        RecipeId as {nameof(Recipe.RecipeId)},
                        UserId as {nameof(Recipe.UserId)},
                        Title as {nameof(Recipe.Title)},
                        Description as {nameof(Recipe.Description)},
                        instructions as {nameof(Recipe.Instuctions)},
                        RecipeURL as {nameof(Recipe.RecipeURL)},
                        DateCreated as {nameof(Recipe.DateCreated)},
                        Notes as {nameof(Recipe.Notes)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Recipe>(sql, new
            {
                recipeId = recipe.RecipeId,
                userId = recipe.UserId,
                title = recipe.Title,
                description = recipe.Description,
                instructions = recipe.Instuctions,
                recipeURL = recipe.RecipeURL,
                dateCreated = recipe.DateCreated,
                notes = recipe.Notes
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
    
 public Recipe updateRecipe(Recipe recipe)
     {
         recipe.DateCreated = date;
          using (var conn = DataConnection.DataSource.OpenConnection())
          {
                var sql = $@"UPDATE recipes SET title = @title, description = @description, instructions = @instructions, recipeurl = @recipeURL, datecreated = @dateCreated, notes = @notes
                            WHERE recipeId = @id
                            RETURNING
                            RecipeId as {nameof(Recipe.RecipeId)},
                            UserId as {nameof(Recipe.UserId)},
                            Title as {nameof(Recipe.Title)},
                            Description as {nameof(Recipe.Description)},
                            Instructions as {nameof(Recipe.Instuctions)},
                            RecipeURL as {nameof(Recipe.RecipeURL)},
                            DateCreated as {nameof(Recipe.DateCreated)},
                            Notes as {nameof(Recipe.Notes)};";
                return conn.QueryFirst<Recipe>(sql, new
                {
                 id = recipe.RecipeId,
                 title = recipe.Title,
                 description = recipe.Description,
                 instructions = recipe.Instuctions,
                 recipeURL = recipe.RecipeURL,
                 dateCreated = recipe.DateCreated,
                 notes = recipe.Notes
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
        var sql = $@"SELECT recipeid, userid, title, description, CAST(instructions AS TEXT) AS instructions, recipeurl, datecreated, notes
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
                    Instuctions  = reader.GetString(reader.GetOrdinal("instructions")),
                    RecipeURL = reader.GetString(reader.GetOrdinal("recipeurl")),
                    DateCreated = reader.GetString(reader.GetOrdinal("datecreated")),
                    Notes = reader.GetString(reader.GetOrdinal("notes"))
                };

                return recipe;
            }
            else
            {
                return null;
            }
        }
    }
    
}