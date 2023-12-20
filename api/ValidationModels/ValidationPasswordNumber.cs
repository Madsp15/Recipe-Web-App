using System.ComponentModel.DataAnnotations;

namespace Recipe_Web_App.ValidationModels;

public class ValidationPasswordNumber : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? password, ValidationContext validationContext)
    {
        if (password is string)
        {
            if((password as string).Any(char.IsDigit)){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Password must contain at least one number.");
    }
}