using infrastructure;
using infrastructure.Models;
using infrastructure.Repositories;

namespace service;


public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly PasswordRepository _passwordRepository;
    private readonly RecipeService _recipeService;

    public UserService(UserRepository userRepository, PasswordRepository passwordRepository, RecipeService recipeService)
    {
        _userRepository = userRepository;
        _passwordRepository = passwordRepository;
        _recipeService = recipeService;
    }

    public User CreateUser(User user)
    {
        return _userRepository.CreateUser(user);
    }

    public User UpdateUser(User user)
    {
        return _userRepository.UpdateUser(user);
    }
    public User UpdateAccount(User user)
    {
        return _userRepository.UpdateAccount(user);
    }

    public bool DeleteUser(int userId)
    {
        if (_passwordRepository.DeletePassword(userId)==true)
        {
            if (_recipeService.DeleteAllRecipesFromUser(userId)==true)
            {
                return _userRepository.DeleteUserById(userId);
            }
        }
        return false;
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