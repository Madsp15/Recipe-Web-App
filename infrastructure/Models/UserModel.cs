namespace infrastructure.Models;

public class User
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public bool IsAdmin { get; set; }
    public string? MoreInfo { get; set; }
    public string? UserAvatarUrl { get; set; }
     
}