using Dapper;

namespace infrastructure;

public class PasswordRepository
{
    public void Create(int userId, string password, string salt)
    {
        const string sql = $@"
        INSERT INTO security(userid, password, salt)
        VALUES (@userId, @password, @salt)";
        using var connection = DataConnection.DataSource.OpenConnection();
        connection.Execute(sql, new { userId, password, salt});
    }
    public bool Deletepassword(int userId)
    {
        const string sql = $@"
        DELETE FROM security
        WHERE userid = @userId";
        using var connection = DataConnection.DataSource.OpenConnection();
        return connection.Execute(sql, new { userId }) > 0;
    }
    
    public void Update(int userId, string password, string salt)
    {
        const string sql = $@"
        UPDATE security
        SET password = @password, salt = @salt
        WHERE userid = @userId";
        using var connection = DataConnection.DataSource.OpenConnection();
        connection.Execute(sql, new { userId, password, salt});
    }
    
    public Password GetByEmail(string email)
    {
        const string sql = $@"
        SELECT 
        security.userid as {nameof(Password.UserId)},
        security.password as {nameof(Password.Hash)},
        security.salt as {nameof(Password.Salt)}
        FROM security
        JOIN users ON security.userid = users.userid
        WHERE email = @email;";
        using var connection = DataConnection.DataSource.OpenConnection();
        return connection.QuerySingle<Password>(sql, new { email });
    }
}
