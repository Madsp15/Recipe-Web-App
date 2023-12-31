using service;
using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App;
using Recipe_Web_App.Filters;
using Recipe_Web_App.TransferModels;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;
    private readonly JWTTokenService _jwtTokenService;
	
	private readonly BlobService _BlobService;
    public UserController(UserService userService, PasswordService passwordService, BlobService blobService, JWTTokenService jwtTokenService)
    {
        _userService = userService;
        _passwordService = passwordService;
		_BlobService = blobService;
        _jwtTokenService = jwtTokenService;
       
	}
    

    [Route("/api/users")]
    [HttpPost]
    public User CreateUser([FromBody] RegisterDto dto)
    {
        User createdUser = new User
        {
            UserName = dto.Username,
            Email = dto.Email,
            MoreInfo = "Click here to write a short description about yourself"
        };
        
        var user = _userService.CreateUser(createdUser);
        _passwordService.Register(user, dto.Password);

        return user;
    }


    [Route("/api/users/{userId}")]
    [HttpPut]
    public User UpdateUser([FromBody] User user, [FromRoute] int userId)
    {
        user.UserId = userId;
        return _userService.UpdateUser(user);
    }
    
    [Route("/api/account/email/{userId}")]
    [HttpPut]
    public User UpdateAccount([FromBody] EmailDto dto, [FromRoute] int userId)
    {
        
        User user = _userService.GetUser(userId);
        user.Email = dto.Email;
        return _userService.UpdateAccount(user);
    }
    
    [Route("/api/account/Username/{userId}")]
    [HttpPut]
    public User UpdateAccount([FromBody] UserNameDto dto, [FromRoute] int userId)
    {
        User user = _userService.GetUser(userId);
        user.UserName = dto.Username;
        return _userService.UpdateAccount(user);
    }
    
    [Route("/api/users/{userId}")]
    [HttpDelete]
    public IActionResult DeleteUser([FromRoute] int userId)
    {
        if (_userService.GetUser(userId) == null)
        {
            return NotFound("User not found");
        }
        if (_userService.DeleteUser(userId))
        {
            return Ok("User deleted");
        } else return BadRequest("User could not be deleted");
        
    }

    [Route("/api/users")]
    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [Route("/api/users/{userId}")]
    [HttpGet]
    public User GetUser([FromRoute] int userId)
    {
        return _userService.GetUser(userId);
    }
    
    [HttpPost]
    [Route("/api/users/login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _passwordService.Authenticate(dto.Email, dto.Password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }
        else
        {
            var token = _jwtTokenService.IssueToken(SessionData.FromUser(user!));
            return Ok(new { token });
        }
    }
	
    
    [HttpPut]
    [Route("/api/users/{userId}/avatar/")]
    public IActionResult UploadAvatar([FromRoute] int userId, IFormFile? avatar)
    {
        if (avatar == null)
        {
            return BadRequest("No file was uploaded, when you needed the avatar the most he disappeared");
        }
        var user = _userService.GetUser(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        string? blobURL = _BlobService.SaveWithSecretURL(avatar.OpenReadStream(), userId);
        
        user.UserAvatarUrl = blobURL;
        _userService.UpdateUser(user);
        return Ok();
    }
    
    [RequireAuthentication]
    [HttpGet]
    [Route("/api/users/whoami")]
    public User WhoAmI()
    {
        var data = HttpContext.GetSessionData();
        Console.WriteLine(data);
        var user = _userService.GetUser(data.UserId);
        return user;
    }
}