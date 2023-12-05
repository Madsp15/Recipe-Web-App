using service;
using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Recipe_Web_App.TransferModels;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;
	
	private readonly BlobService _BlobService;
    public UserController(UserService userService, PasswordService passwordService, BlobService blobService)
    {
        _userService = userService;
        _passwordService = passwordService;
		_BlobService = blobService;
	}
    

    [Route("/api/users")]
    [HttpPost]
    public User CreateUser([FromBody] RegisterDto dto)
    {
        User createdUser = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
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

    [Route("/api/users/{userId}")]
    [HttpGet]
    public User GetUser([FromRoute] int userId)
    {
        return _userService.GetUser(userId);
    }

    [Route("/api/users/{userId}/follow/{userIdToFollow}")]
    [HttpPost]
    public bool FollowUser([FromRoute] int userId, [FromRoute] int userIdToFollow)
    {
        return _userService.FollowUser(userId, userIdToFollow);
    }

    [Route("/api/users/{userId}/unfollow/{userIdToUnfollow}")]
    [HttpDelete]
    public bool UnfollowUser([FromRoute] int userId, [FromRoute] int userIdToUnfollow)
    {
        return _userService.UnfollowUser(userId, userIdToUnfollow);
    }

    [Route("/api/users/{userId}/followers")]
    [HttpGet]
    public IEnumerable<User> GetFollowers([FromRoute] int userId)
    {
        return _userService.GetFollowers(userId);
    }

    [Route("/api/users/{userId}/following")]
    [HttpGet]
    public IEnumerable<User> GetFollowing([FromRoute] int userId)
    {
        return _userService.GetFollowing(userId);
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
	
    
    [HttpPut]
    [Route("/api/users/{userId}/avatar")]
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

}