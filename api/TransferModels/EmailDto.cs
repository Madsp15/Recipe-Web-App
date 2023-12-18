using System.ComponentModel.DataAnnotations;
using Recipe_Web_App.ValidationModels;
namespace Recipe_Web_App.TransferModels;

public class EmailDto
{
    [Required] [ValidationEmail] [ValidationEmailExist] public required string Email { get; set; }
    
}