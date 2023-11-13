using service;
using infrastructure;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UserController : ControllerBase
{
    private readonly Service _service;

    public UserController(Service service)
    {
        _service = service;
    }

[Route("api/users")]
    [HttpPost]
    public User CreateUser([FromBody] User user)
    {
    
        return _service.CreateUser(user);
    }
}