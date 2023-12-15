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
    
    public bool DeleteTag(int id)
    {
        var sql = $@"DELETE FROM tags WHERE tagId = @id;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id }) == 1;
        }
    }
    
    public Tag UpdateTag(Tag tag)
    {
        var sql = $@"UPDATE tags
                        SET tagname = @tagname
                        WHERE tagId = @tagId
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
    public Tag GetTagById(int id)
    {
        var sql = $@"SELECT * FROM tags
                        WHERE tagId = @id;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Tag>(sql, new { id });
        }
    }
    
    /*public bool AddTagToRecipe(int tagId, int recipeId)
    {
        var sql = $@"INSERT INTO recipeTags(tagId, recipeId)
                        VALUES(@tagId, @recipeId);";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { tagId, recipeId }) == 1;
        }
    } */
    
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
    
    public bool DeleteTagFromRecipe(int tagId, int recipeId)
    {
        var sql = $@"DELETE FROM recipeTags WHERE tagId = @tagId AND recipeId = @recipeId;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { tagId, recipeId }) == 1;
        }
    }
    public bool CheckIfTagExists(string name)
    {
        var sql = $@"SELECT * FROM tags
                        WHERE tagname = @name;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            try
            {
                if (conn.QueryFirst<Tag>(sql, new { name })==null)
                {
                    return false;
                }
                return conn.QueryFirst<Tag>(sql, new { name }) != null;
            }
            catch (Exception)
            {
                return false;
            }
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
}