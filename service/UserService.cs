using infrastructure;
namespace service;


public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly PasswordHashAlgorithm _passwordHashAlgorithm;

    public UserService(UserRepository userRepository, PasswordHashAlgorithm passwordHashAlgorithm)
    {
        _userRepository = userRepository;
        _passwordHashAlgorithm = passwordHashAlgorithm;
    }

    public User CreateUser(User user)
    {
        string salt = _passwordHashAlgorithm.GenerateSalt();
        string hashedPassword = _passwordHashAlgorithm.HashPassword(user.Password, salt);
        user.Salt = salt;
        user.Password = hashedPassword;
        Console.WriteLine("Salt: "+salt);
        Console.WriteLine("Password: "+hashedPassword);
        return _userRepository.CreateUser(user);
    }

    public User UpdateUser(User user)
    {
        return _userRepository.UpdateUser(user);
    }

    public bool DeleteUser(int userId)
    {
        return _userRepository.DeleteUserById(userId);
    }

    public User GetUser(int userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public bool VerifyUser(string email, string password)
    {
        User user = _userRepository.GetUserByEmail(email);
        
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }
        
        if (user == null)
        {
            return false;
        }
        
        bool passwordMatch = _passwordHashAlgorithm.VerifyHashedPassword(email, password, user.Password, user.Salt);
        
        if (!passwordMatch)
        {
            return false;
        }

        return true;
    }
}