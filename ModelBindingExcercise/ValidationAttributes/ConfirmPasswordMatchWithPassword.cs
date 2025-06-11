using ModelBindingExcercise.Models;
using System.ComponentModel.DataAnnotations;

namespace ModelBindingExcercise.ValidationAttributes
{
    public class ConfirmPasswordMatchWithPassword : ValidationAttribute   
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var registrationDto = validationContext.ObjectInstance as RegistrationDto;
            if (registrationDto != null && 
                !string.IsNullOrWhiteSpace(registrationDto.Password) && 
                !string.IsNullOrWhiteSpace(registrationDto.ConfirmPassword) &&
                !registrationDto.ConfirmPassword.Equals(registrationDto.Password)) 
            {
                return new ValidationResult("Password and confirm passwords should match !");
            }
            return ValidationResult.Success;
        }
    }
}
