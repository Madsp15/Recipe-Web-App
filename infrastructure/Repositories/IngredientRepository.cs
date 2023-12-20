using Dapper;
using infrastructure.Models;

namespace infrastructure.Repositories;

public class IngredientRepository
{
    public Ingredient CreateIngredient(Ingredient ingredient)
    {
        var sql = $@"INSERT INTO ingredients(ingredientName)
                        VALUES(@ingredientname)
                        RETURNING
                        ingredientId as {nameof(Ingredient.IngredientId)},
                        ingredientName as {nameof(Ingredient.IngredientName)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Ingredient>(sql, new
            {
                ingredientid = ingredient.IngredientId, ingredientname = ingredient.IngredientName,

            });
        }
    }
  
    public Ingredient GetIngredientByName(string name)
    {
        var sql = $@"SELECT ingredientid, ingredientname FROM ingredients
                        WHERE ingredientName = @name;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<Ingredient>(sql, new
            {
                name
            });
        }
    }

    public IEnumerable<Ingredients> GetAllIngredientsFromRecipe(int recipeId)
    {
        var sql = $@"SELECT * FROM ingredients
                        JOIN recipeingredients ON ingredients.ingredientId = recipeingredients.ingredientId
                        WHERE recipeingredients.recipeId = @recipeId;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Ingredients>(sql, new
            {
                recipeId
            });
        }
    }


    public RecipeIngredient CreateRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        var sql = $@"INSERT INTO recipeingredients(recipeId, ingredientId, quantity, unit)
                        VALUES(@recipeId, @ingredientId, @quantity, @unit)
                        RETURNING
                        recipeIngredientId as {nameof(RecipeIngredient.RecipeIngredientId)},
                        recipeId as {nameof(RecipeIngredient.RecipeId)},
                        ingredientId as {nameof(RecipeIngredient.IngredientId)},
                        quantity as {nameof(RecipeIngredient.Quantity)},
                        unit as {nameof(RecipeIngredient.Unit)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<RecipeIngredient>(sql, new
            {
                recipeingredientid = recipeIngredient.RecipeIngredientId,
                recipeid = recipeIngredient.RecipeId,
                ingredientid = recipeIngredient.IngredientId,
                quantity = recipeIngredient.Quantity,
                unit = recipeIngredient.Unit,

            });
        }
    }
    
    public bool DeleteRecipeIngredient(int ingredientId, int recipeId)
    {
        var sql = $@"DELETE FROM recipeingredients WHERE recipeId = @recipeId AND ingredientId = @ingredientId";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new
            {
                ingredientId, recipeId
            }) == 1;
        }
    }
    
    public bool DeleteRecipeIngredient(int recipeId)
    {
        var sql = $@"DELETE FROM recipeingredients WHERE recipeId = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id = recipeId }) == 1;
        }
    }

    public IEnumerable<Ingredient> GetAllIngredients()
    {
        var sql = $@"SELECT * FROM ingredients
                        ORDER BY ingredientName;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Ingredient>(sql);
        }
    }
  
    public bool DeleteIngredientsFromRecipe(int recipeId)
    {
        var sql = $@"DELETE FROM recipeingredients WHERE recipeId = @recipeId;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new
            {
                recipeId
            }) == 1;
        }
    }
    public bool DeleteIngredientsFromRecipeByName(int ingreddientid, int recipeId)
    {
        var sql = $@"DELETE FROM recipeingredients WHERE recipeId = @recipeId AND ingredientid = @ingreddientid;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { ingreddientid, recipeId}) == 1;
        }
    }
}


