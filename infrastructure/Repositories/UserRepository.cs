using Dapper;
using infrastructure.Models;

namespace infrastructure.Repositories;

public class UserRepository
{
    public User CreateUser(User user)
    {
        var sql = $@"INSERT INTO users(username, type, moreinfo, email)
                        VALUES(@username, @type, @moreinfo, @email)
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        type as {nameof(User.Type)},
                        email as {nameof(User.Email)},
                        moreInfo as {nameof(User.MoreInfo)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                username = user.UserName,
                type = user.Type,
                email = user.Email,
                moreinfo = user.MoreInfo

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
                    Type = @type,
                    Email = @email,
                    MoreInfo = @moreinfo
                 WHERE
                    userid = @id
                 RETURNING
                    userId as {nameof(User.UserId)},
                    username as {nameof(User.UserName)},
                    type as {nameof(User.Type)},
                    email as {nameof(User.Email)},
                    moreinfo as {nameof(User.MoreInfo)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                id = user.UserId,
                username = user.UserName,
                type = user.Type,
                email = user.Email,
                moreinfo = user.MoreInfo
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
}