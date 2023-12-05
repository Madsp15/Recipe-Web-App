using Dapper;
using infrastructure.Models;

namespace infrastructure.Repositories;

public class UserRepository
{
    public User CreateUser(User user)
    {
        user.UserAvatarUrl = "http://placekitten.com/200/200";
        var sql = $@"INSERT INTO users(username, isadmin, moreinfo, email, useravatarurl)
                        VALUES(@username, @isadmin, @moreinfo, @email, @useravatarurl)
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        isadmin as {nameof(User.Isadmin)},
                        email as {nameof(User.Email)},
                        moreInfo as {nameof(User.MoreInfo)},
                        useravatarurl as {nameof(User.UserAvatarUrl)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                username = user.UserName,
                isadmin = user.Isadmin,
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
        var sql = $@"UPDATE users
                 SET
                    username = @username,
                    isadmin = @isadmin,
                    Email = @email,
                    MoreInfo = @moreinfo,
                    useravatarurl = @useravatarurl
                 WHERE
                    userid = @id
                 RETURNING
                    userId as {nameof(User.UserId)},
                    username as {nameof(User.UserName)},
                    isadmin as {nameof(User.Isadmin)},
                    email as {nameof(User.Email)},
                    moreinfo as {nameof(User.MoreInfo)},
                    useravatarurl as {nameof(User.UserAvatarUrl)};";
        

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                id = user.UserId,
                username = user.UserName,
                isadmin = user.Isadmin,
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
}