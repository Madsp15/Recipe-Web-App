using Dapper;
using infrastructure.Models;

namespace infrastructure.Repositories;

public class UserRepository
{
    public User CreateUser(User user)
    {
        user.IsAdmin = false;
        user.UserAvatarUrl = "http://placekitten.com/200/200";
        var sql = $@"INSERT INTO users(username, isadmin, moreinfo, email, useravatarurl)
                        VALUES(@username, @isadmin, @moreinfo, @email, @useravatarurl)
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        isadmin as {nameof(User.IsAdmin)},
                        email as {nameof(User.Email)},
                        moreInfo as {nameof(User.MoreInfo)},
                        useravatarurl as {nameof(User.UserAvatarUrl)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                username = user.UserName,
                isadmin = user.IsAdmin,
                email = user.Email,
                moreinfo = user.MoreInfo,
                useravatarurl = user.UserAvatarUrl

            });
        }
    }
    
    public User GetUserById(int userId)
    {
        var sql = $@"SELECT * FROM users WHERE userid = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { id=userId });
        }
    }
    
    public bool DeleteUserById(int userId)
    {
        var sql = $@"DELETE FROM users WHERE userid = @id;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { id = userId }) == 1;
        }
    }

    public User UpdateUser(User user)
    {
        Console.WriteLine("User: "+user);
        var sql = $@"UPDATE users SET username = @username, email = @email, moreinfo = @moreinfo, useravatarurl = @useravatarurl
                        WHERE userid = @id
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        isadmin as {nameof(User.IsAdmin)},
                        email as {nameof(User.Email)},
                        moreInfo as {nameof(User.MoreInfo)},
                        useravatarurl as {nameof(User.UserAvatarUrl)};";
    

    using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                id = user.UserId,
                username = user.UserName,
                isadmin = user.IsAdmin,
                email = user.Email,
                moreinfo = user.MoreInfo,
                useravatarurl = user.UserAvatarUrl
            });
        }
    }
    public User UpdateAccount(User user)
    {
        Console.WriteLine("User: "+user);
        var sql = $@"UPDATE users SET username = @username, email = @email
                        WHERE userid = @id
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        isadmin as {nameof(User.IsAdmin)},
                        email as {nameof(User.Email)},
                        moreInfo as {nameof(User.MoreInfo)},
                        useravatarurl as {nameof(User.UserAvatarUrl)};";
        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                id = user.UserId,
                username = user.UserName,
                isadmin = user.IsAdmin,
                email = user.Email,
                moreinfo = user.MoreInfo,
                useravatarurl = user.UserAvatarUrl
            });
        }
    }
        
    public IEnumerable<User> GetAllUsers()
    {
            var sql = $@"SELECT * FROM users";
    
            using (var conn = DataConnection.DataSource.OpenConnection())
            {
                return conn.Query<User>(sql);
            }
    }
    
    public bool DoesUsernameExist(string? username)
    {
        var sql = "SELECT count(*) from users where username = @Username;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            var parameters = new { Username = username };
            return conn.ExecuteScalar<int>(sql, parameters) > 0;
        }
    }
    
    public bool DoesEmailExist(string? email)
    {
        var sql = "SELECT count(*) from users where email = @Email;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            var parameters = new { Email = email };
            return conn.ExecuteScalar<int>(sql, parameters) > 0;
        }
    }
    
    public bool DeleteSavedRecipeFromUsers(int recipeId)
    {
        var sql = $@"DELETE FROM savedposts WHERE recipeid = @recipeid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { recipeid = recipeId }) == 1;
        }
    }
    
}