using Dapper;

namespace infrastructure;

public class TagsRepository
{
    public Tag CreateTag(Tag tag)
    {
        var sql = $@"INSERT INTO tags(tagname)
                        VALUES(@tagname)
                        RETURNING
                        tagId as {nameof(Tag.tagId)},
                        tagname as {nameof(Tag.tagName)};";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Tag>(sql, new
            {
                tagid = tag.tagId,
                tagname = tag.tagName,
                
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
                        tagId as {nameof(Tag.tagId)},
                        tagname as {nameof(Tag.tagName)};";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Tag>(sql, new
            {
                tagid = tag.tagId,
                tagname = tag.tagName,
                
            });
        }
    }
    
    public Tag GetTagByName(string name)
    {
        var sql = $@"SELECT * FROM tags
                        WHERE tagname = @name;";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<Tag>(sql, new { name });
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
    
    public bool AddTagToRecipe(int tagId, int recipeId)
    {
        var sql = $@"INSERT INTO recipeTags(tagId, recipeId)
                        VALUES(@tagId, @recipeId);";
        
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { tagId, recipeId }) == 1;
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
            return conn.QueryFirst<Tag>(sql, new { name }) != null;
        }
    }
    
}