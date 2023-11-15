using Dapper;
namespace infrastructure;

public class UserRepository
{
    public User CreateUser(User user)
    {
        var sql = $@"INSERT INTO users(username, type, email, password, salt, moreinfo)
                        VALUES(@username, @type, @email, @password, @salt, @moreinfo)
                        RETURNING
                        userId as {nameof(User.UserId)},
                        username as {nameof(User.UserName)},
                        type as {nameof(User.Type)},
                        email as {nameof(User.Email)},
                        password as {nameof(User.Password)},
                        salt as {nameof(User.Salt)},
                        moreInfo as {nameof(User.MoreInfo)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                username = user.UserName,
                type = user.Type,
                email = user.Email,
                password = user.Password,
                salt = user.Salt,
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
                    Password = @password,
                    Salt = @salt,
                    MoreInfo = @moreinfo
                 WHERE
                    userid = @id
                 RETURNING
                    userId as {nameof(User.UserId)},
                    username as {nameof(User.UserName)},
                    type as {nameof(User.Type)},
                    email as {nameof(User.Email)},
                    password as {nameof(User.Password)},
                    salt as {nameof(User.Salt)},
                    moreinfo as {nameof(User.MoreInfo)};";

        using (var conn = DataConnection.DataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                id = user.UserId,
                username = user.UserName,
                type = user.Type,
                email = user.Email,
                password = user.Password,
                salt = user.Salt,
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