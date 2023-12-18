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
    
    public bool FollowUser(int userId, int followerId)
    {
        
        if (IsFollowing(userId, followerId))
        {
            return true;
        }else
        {
            var sql = $@"INSERT INTO followers(userid, followerid)
                        VALUES(@userid, @followerid);";

            using (var conn = DataConnection.DataSource.OpenConnection())
            {
                return conn.Execute(sql, new { userid = userId, followerid = followerId }) == 1;
            }
        }
        
    }
    public bool UnfollowUser(int userId, int followerId)
    {
        var sql = $@"DELETE FROM followers WHERE userid = @userid AND followerid = @followerid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { userid = userId, followerid = followerId }) == 1;
        }
    }
    
    public IEnumerable<User> GetFollowers(int userId)
    {
        var sql = $@"SELECT * FROM followers
                        INNER JOIN users ON followers.followerid = users.userid
                        WHERE followers.userid = @userid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<User>(sql, new { userid = userId });
        }
    }
    
    public IEnumerable<User> GetFollowing(int userId)
    {
        var sql = $@"SELECT * FROM users
                        INNER JOIN followers ON users.userid = followers.userid
                        WHERE followers.followerid = @userid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Query<User>(sql, new { userid = userId });
        }
    }
    
    
    public bool IsFollowing(int userId, int followerId)
    {
        var sql = $@"SELECT * FROM followers
                        WHERE userid = @userid AND followerid = @followerid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault(sql, new { userid = userId, followerid = followerId }) != null;
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
    
    public bool saveRecipe(int userId, int recipeId)
    {
        var sql = $@"INSERT INTO savedposts(userid, recipeid)
                        VALUES(@userid, @recipeid);";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { userid = userId, recipeid = recipeId }) == 1;
        }
    }
    
    public bool unsaveRecipe(int userId, int recipeId)
    {
        var sql = $@"DELETE FROM savedposts WHERE userid = @userid AND recipeid = @recipeid;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.Execute(sql, new { userid = userId, recipeid = recipeId }) == 1;
        }
    }
    
    public bool IsAdmin(int userId)
    {
        var sql = $@"SELECT * FROM users
                        WHERE userid = @userid AND isadmin = true;";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault(sql, new { userid = userId }) != null;
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