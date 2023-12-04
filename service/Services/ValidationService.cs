using infrastructure.Repositories;

namespace service;

public class ValidationService
{
    private readonly UserRepository _userRepository;

    public ValidationService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsUsernameValid(string? username)
    {
        if (_userRepository.DoesUsernameExist(username))
        {
            return false;
        }

        if (username.Length < 3)
        {
            return false;
        }

        if (username.Length > 20)
        {
            return false;
        }

        if (!username.All(char.IsLetterOrDigit))
        {
            return false;
        }

        return true;
    }

    public bool IsPasswordValid(string password)
    {
        if (password.Length < 8)
        {
            return false;
        }

        if (!password.All(char.IsLetterOrDigit))
        {
            return false;
        }

        if (!password.Any(char.IsDigit))
        {
            return false;
        }

        return true;
    }
}