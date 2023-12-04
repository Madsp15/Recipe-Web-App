using service;
using infrastructure;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App.TransferModels;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;

    public UserController(UserService userService, PasswordService passwordService)
    {
        _userService = userService;
        _passwordService = passwordService;
    }

    [Route("/api/users")]
    [HttpPost]
    public User CreateUser([FromBody] RegisterDto dto)
    {
        User createdUser = _userService.CreateUser(dto.User);
        _passwordService.Register(createdUser, dto.Password);

        return createdUser; 
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
    public ResponseDto Login([FromBody] LoginDto dto)
    {
        var user = _passwordService.Authenticate(dto.Email, dto.Password);
        return new ResponseDto
        {
            MessageToClient = "Successfully authenticated"
        };
    }
}