using System.ComponentModel.DataAnnotations;
using infrastructure;

namespace Recipe_Web_App.TransferModels;

public class RegisterDto
{
    public User User { get; set; }

    [Required] [MinLength(8)] public required string Password { get; set; }
    
}