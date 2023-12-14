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
                ingredientid = ingredient.IngredientId,
                ingredientname = ingredient.IngredientName,
                
            });
        }
    }
    public bool DeleteIngredient(int id)
    {
        var sql = $@"DELETE FROM ingredients WHERE ingredientId = @id;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id }) == 1;
        }
    }
    public Ingredient UpdateIngredient(Ingredient ingredient)
    {
        var sql = $@"UPDATE ingredients
                        SET ingredientName = @ingredientname
                        WHERE ingredientId = @ingredientId
                        RETURNING
                        ingredientId as {nameof(Ingredient.IngredientId)},
                        ingredientName as {nameof(Ingredient.IngredientName)};";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Ingredient>(sql, new
            {
                ingredientid = ingredient.IngredientId,
                ingredientname = ingredient.IngredientName,
                
            });
        }
    }
    
    public Ingredient GetIngredientByName(string name)
    {
        var sql = $@"SELECT ingredientid, ingredientname FROM ingredients
                        WHERE ingredientName = @name;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<Ingredient>(sql, new { name });
        }
    }
    
    public IEnumerable<Ingredients> GetAllIngredientsFromRecipe(int recipeId)
    {
        var sql = $@"SELECT * FROM ingredients
                        JOIN recipeingredients ON ingredients.ingredientId = recipeingredients.ingredientId
                        WHERE recipeingredients.recipeId = @recipeId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Ingredients>(sql, new { recipeId });
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
    public bool DeleteRecipeIngredient(int id)
    {
        var sql = $@"DELETE FROM recipeingredients WHERE recipeIngredientId = @id;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id }) == 1;
        }
    }
    
    public RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
    {
        var sql = $@"UPDATE recipeingredients
                        SET recipeId = @recipeId, ingredientId = @ingredientId, quantity = @quantity, unit = @unit
                        WHERE recipeIngredientId = @recipeIngredientId
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
    
    public RecipeIngredient GetRecipeIngredientById(int id)
    {
        var sql = $@"SELECT * FROM recipeingredients
                        WHERE recipeIngredientId = @id;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<RecipeIngredient>(sql, new { id });
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
    public IEnumerable<Ingredient>SearchForIngredient(string search)
    {
        var sql = $@"SELECT * FROM ingredients
                        WHERE ingredientName LIKE @search;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Ingredient>(sql, new { search = $"%{search}%" });
        }
    }
}