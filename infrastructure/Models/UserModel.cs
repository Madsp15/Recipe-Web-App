namespace infrastructure.Models;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Isadmin { get; set; }
    public string MoreInfo { get; set; }
    public string? UserAvatarUrl { get; set; }
     
}