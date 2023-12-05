using System.ComponentModel.DataAnnotations;

namespace Recipe_Web_App.ValidationModels;

public class ValidationPasswordSpecialCharacter : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? password, ValidationContext validationContext)
    {
        if (password is string)
        {
            if((password as string).Any(c => !char.IsLetterOrDigit(c))){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Password must contain at least one special character. Valid special characters include: ! @ # $ % ^ & * ( ) _ + - = { } [ ] | \\\\ : ; , . ? /");
    }
}