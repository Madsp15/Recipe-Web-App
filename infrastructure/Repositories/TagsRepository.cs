using Dapper;
using infrastructure.Models;

namespace infrastructure;

public class TagsRepository
{
    public Tag CreateTag(Tag tag)
    {
        var sql = $@"INSERT INTO tags(tagname)
                        VALUES(@tagname)
                        RETURNING
                        tagId as {nameof(Tag.TagId)},
                        tagname as {nameof(Tag.TagName)};";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Tag>(sql, new
            {
                tagid = tag.TagId,
                tagname = tag.TagName,
                
            });
        }
    }
    
    
    public Tag GetTagByName(string name)
    {
        var sql = $@"SELECT tagid, tagname FROM tags
                    WHERE tagname = @name;";
    
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<Tag>(sql, new { name });
        }
    }

    
    public List<Tag> GetAllTags()
    {
        var sql = $@"SELECT * FROM tags
                        ORDER BY tagname;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Tag>(sql).ToList();
        }
    }
    
    public bool AddTagsToRecipe(int recipeId, List<int> tagIds)
    {
        var sql = $@"INSERT INTO recipeTags(tagId, recipeId)
                 VALUES (@tagId, @recipeId);";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            int affectedRows = 0;

            foreach (int tagId in tagIds)
            {
                affectedRows += conn.Execute(sql, new { tagId, recipeId });
            }
            
            return affectedRows > 0;
        }
    }
    
    public bool AddTagToRecipe(int tagId, int recipeId)
    {
        var sql = @"INSERT INTO recipeTags (tagId, recipeId)
                VALUES (@tagId, @recipeId);";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            int affectedRows = conn.Execute(sql, new { tagId, recipeId });
            return affectedRows == 1;
        }
    }
    
    public List<Tag> GetTagsByRecipeId(int recipeId)
    {
        var sql = $@"SELECT * FROM tags
                        JOIN recipeTags ON tags.tagId = recipeTags.tagId
                        WHERE recipeTags.recipeId = @recipeId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<Tag>(sql, new { recipeId }).ToList();
        }
    }

    public List<string> GetTagNamesByRecipeId(int recipeId)
    {
        var sql = $@"SELECT tags.TagName
                            FROM tags
                            JOIN recipetags ON tags.TagId = recipetags.TagId
                            WHERE recipetags.RecipeId = @recipeId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<string>(sql, new { recipeId }).ToList();
        }
    }
    
    public bool DeleteTagsFromRecipe(int recipeId)
    {
        var sql = $@"DELETE FROM recipeTags WHERE recipeId = @recipeId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { recipeId }) == 1;
        }
    }

    public bool DeleteTagByName(int tagId, int recipeId)
    {
        var sql = $@"DELETE FROM recipeTags WHERE recipeId = @recipeId AND tagId = @tagId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { tagId, recipeId}) == 1;
        }
    }
}