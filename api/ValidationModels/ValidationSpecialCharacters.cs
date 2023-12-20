using System.ComponentModel.DataAnnotations;

namespace Recipe_Web_App.ValidationModels;

public class ValidationSpecialCharacters : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string)
        {
            if((value as string).All(char.IsLetterOrDigit)){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Username cannot contain special characters");
    }
}


