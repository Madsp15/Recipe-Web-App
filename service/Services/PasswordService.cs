using infrastructure;
using infrastructure.Models;
using infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace service;

public class PasswordService
{
    private readonly ILogger<PasswordService> _logger;
    private readonly PasswordHashAlgorithm _passwordHashAlgorithm;
    private readonly PasswordRepository _passwordRepository;
    private readonly UserRepository _userRepository;
    
    public PasswordService(PasswordHashAlgorithm passwordHashAlgorithm, PasswordRepository passwordRepository, UserRepository userRepository, ILogger<PasswordService> logger)
    {
        _passwordHashAlgorithm = passwordHashAlgorithm;
        _passwordRepository = passwordRepository;
        _userRepository = userRepository;
        _logger = logger;
    }
    
    
    public User? Authenticate(string email, string password)
    {

        try
        {
            var passwordHash = _passwordRepository.GetByEmail(email);

            if (_passwordHashAlgorithm.VerifyHashedPassword(email, password, passwordHash.Hash, passwordHash.Salt))
            {
                _userRepository.GetUserById(passwordHash.UserId);
            }

        }
        catch (Exception e)
        {
            _logger.LogError("Authenticate error: {Message}", e);
        }

        return null;
    }

    
    public void Register(User user, string password)
    {
        var salt = _passwordHashAlgorithm.GenerateSalt();
        var hash = _passwordHashAlgorithm.HashPassword(password, salt);
        _passwordRepository.Create(user.UserId, hash, salt);
    }
}