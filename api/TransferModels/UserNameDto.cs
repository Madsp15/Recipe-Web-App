using System.ComponentModel.DataAnnotations;
using Recipe_Web_App.ValidationModels;
namespace Recipe_Web_App.TransferModels;

public class UserNameDto
{
    [Required] [MinLength(4)] [MaxLength(20)] [ValidationUsernameExist] [ValidationSpecialCharacters] public required string Username { get; set; }
}