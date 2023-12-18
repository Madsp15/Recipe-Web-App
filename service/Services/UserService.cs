using infrastructure;
using infrastructure.Models;
using infrastructure.Repositories;

namespace service;


public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly PasswordHashAlgorithm _passwordHashAlgorithm;
    private readonly PasswordRepository _passwordRepository;
    private readonly RecipeService _recipeService;

    public UserService(UserRepository userRepository, PasswordHashAlgorithm passwordHashAlgorithm, PasswordRepository passwordRepository, RecipeService recipeService)
    {
        _userRepository = userRepository;
        _passwordHashAlgorithm = passwordHashAlgorithm;
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
        if (_passwordRepository.Deletepassword(userId)==true)
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

    
    public bool FollowUser(int userId, int userIdToFollow)
    {
        return _userRepository.FollowUser(userId, userIdToFollow);
    }
    
    public bool UnfollowUser(int userId, int userIdToUnfollow)
    {
        return _userRepository.UnfollowUser(userId, userIdToUnfollow);
    }
    
    public IEnumerable<User> GetFollowers(int userId)
    {
        return _userRepository.GetFollowers(userId);
    }
    
    public IEnumerable<User> GetFollowing(int userId)
    {
        return _userRepository.GetFollowing(userId);
    }
    
    public bool SaveRecipe(int userId, int recipeId)
    {
        return _userRepository.saveRecipe(userId, recipeId);
    }
    
    public bool UnsaveRecipe(int userId, int recipeId)
    {
        return _userRepository.unsaveRecipe(userId, recipeId);
    }   
    
   
}