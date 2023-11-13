
using Dapper;
namespace infrastructure;

public class Repository
{
    public User CreateUser(User user)
    {
        var sql = $@"INSERT INTO tables.users(User_Name, Type, Email, Password, Salt, More_Info)
                        VALUES(@username, @type, @email, @password, @salt, @moreinfo)
                        RETURNING
                        User_ID as {nameof(User.Id)},
                        User_Name as {nameof(User.UserName)},
                        Type as {nameof(User.Type)},
                        Email as {nameof(User.Email)},
                        Password as {nameof(User.Password)},
                        Salt as {nameof(User.Salt)},
                        More_Info as {nameof(User.MoreInfo)};";

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
}