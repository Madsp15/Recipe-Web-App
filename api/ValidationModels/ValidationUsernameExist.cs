using System.ComponentModel.DataAnnotations;
using infrastructure.Repositories;

namespace Recipe_Web_App.ValidationModels;

public class ValidationUsernameExist : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? username, ValidationContext validationContext)
    {
        var repo = validationContext.GetService(typeof(UserRepository)) as UserRepository;
        if (username is string)
        {
            if(!repo.DoesUsernameExist(username as string)){
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("This username already exists");
    }
}