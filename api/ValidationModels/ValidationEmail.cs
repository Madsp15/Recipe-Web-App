using System.ComponentModel.DataAnnotations;
using infrastructure.Repositories;

namespace Recipe_Web_App.ValidationModels;

public class ValidationEmail : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? email, ValidationContext validationContext)
    {
        if (email is string)
        {
            if((email as string).Contains("@")){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("This email is not valid");
    }
}