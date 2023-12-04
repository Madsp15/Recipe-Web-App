using infrastructure;

namespace service;

public class PasswordService
{
    private readonly PasswordHashAlgorithm _passwordHashAlgorithm;
    private readonly PasswordRepository _passwordRepository;
    
    public PasswordService(PasswordHashAlgorithm passwordHashAlgorithm, PasswordRepository passwordRepository)
    {
        _passwordHashAlgorithm = passwordHashAlgorithm;
        _passwordRepository = passwordRepository;
    }
    
    public bool? Authenticate(string email, string password)
    {
        var passwordHash = _passwordRepository.GetByEmail(email);
    
        if(_passwordHashAlgorithm.VerifyHashedPassword(email, password, passwordHash.Hash, passwordHash.Salt))
        {
            return true;
        }
        
        return false;
    }

    
    public void Register(User user, string password)
    {
        var salt = _passwordHashAlgorithm.GenerateSalt();
        var hash = _passwordHashAlgorithm.HashPassword(password, salt);
        _passwordRepository.Create(user.UserId, hash, salt);
    }
}