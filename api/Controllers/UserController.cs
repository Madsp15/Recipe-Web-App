using service;
using infrastructure;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App.TransferModels;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Route("/api/users")]
    [HttpPost]
    public User CreateUser([FromBody] User user)
    {
        return _userService.CreateUser(user);
    }

    [Route("/api/users/{userId}")]
    [HttpPut]
    public User UpdateUser([FromBody] User user, [FromRoute] int userId)
    {
        user.UserId = userId;
        return _userService.UpdateUser(user);
    }

    [Route("/api/users/{userId}")]
    [HttpDelete]
    public bool DeleteUser([FromRoute] int userId)
    {
        return _userService.DeleteUser(userId);
    }

    [Route("/api/users")]
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }
    
    [HttpPost]
    [Route("/api/users/login")]
    public bool Login([FromBody] LoginDto dto)
    {
        return _userService.VerifyUser(dto.Email, dto.Password);
    }
}