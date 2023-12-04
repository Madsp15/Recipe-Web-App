using System.ComponentModel.DataAnnotations;
using infrastructure.Repositories;

namespace Recipe_Web_App.ValidationModels;

public class ValidationEmailExist : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? email, ValidationContext validationContext)
    {
        var repo = validationContext.GetService(typeof(UserRepository)) as UserRepository;
        if (email is string)
        {
            if(!repo.DoesEmailExist(email as string)){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("This email already exists");
    }
}