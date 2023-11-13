using infrastructure;
namespace service;


public class Service
{
    private readonly Repository _repository;

    public Service(Repository repository)
    {
        _repository = repository;
    }

    public User CreateUser(User user)
    {
        return _repository.CreateUser(user);
    }
}