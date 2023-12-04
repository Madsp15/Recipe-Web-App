using service;
using infrastructure;
using infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly BlobService _BlobService;

    public UserController(UserService userService, BlobService blobService)
    {
        _userService = userService;
        _BlobService = blobService;
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
    
    [HttpPut]
    [Route("/api/users/{userId}/avatar")]
    public IActionResult UploadAvatar([FromRoute] int userId, IFormFile? avatar)
    {
        if (avatar == null)
        {
            return BadRequest("No file was uploaded");
        }
        var user = _userService.GetUser(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }
        //var url = _BlobService.Save("avatars", avatar.OpenReadStream(), user.);
        //user.Avatar = url;
        _userService.UpdateUser(user);
        return Ok();
        
    }
}