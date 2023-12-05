using System.ComponentModel.DataAnnotations;
using Recipe_Web_App.ValidationModels;
namespace Recipe_Web_App.TransferModels;

public class RegisterDto

{
    [Required] [MinLength(4)] [MaxLength(20)] [ValidationUsernameExist] public required string UserName { get; set; }
    
    [Required] [ValidationEmail] [ValidationEmailExist] public required string Email { get; set; }

    [Required] [MinLength(8)] [ValidationPasswordNumber] [ValidationPasswordSpecialCharacter] public required string Password { get; set; }
    
}