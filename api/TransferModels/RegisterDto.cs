using System.ComponentModel.DataAnnotations;
using Recipe_Web_App.ValidationModels;


public class RegisterDto
{
    [Required] [MinLength(4)] [MaxLength(20)] [ValidationUsernameExist] public string UserName { get; set; }
    
    [Required] [ValidationEmail] [ValidationEmailExist] public string Email { get; set; }

    [Required] [MinLength(8)] public required string Password { get; set; }
    
}