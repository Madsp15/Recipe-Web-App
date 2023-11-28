using infrastructure;
using infrastructure.Models;
using infrastructure.Repositories;

namespace service;


public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User CreateUser(User user)
    {
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
}