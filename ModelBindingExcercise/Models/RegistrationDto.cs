using ModelBindingExcercise.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ModelBindingExcercise.Models
{
    public class RegistrationDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be minimum of 6 and maxmimum of 100 characters.")]
        [MaxLength(100, ErrorMessage = "Password must be minimum of 6 and maxmimum of 100 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [MinLength(6)]
        [ConfirmPasswordMatchWithPassword]
        public string? ConfirmPassword { get; set; }

        public override string ToString()
        {
            return $"Testing - {Email} - {Password} - {ConfirmPassword}";
        }


        // custom binding to use HTTP Get and query strings
        public static ValueTask<RegistrationDto?> BindAsync(HttpContext context)
        {
            var email = context.Request.Query["email"];
            var password = context.Request.Query["pw1"];
            var confirmPassword = context.Request.Query["pw2"];
            return new ValueTask<RegistrationDto?>(
                new RegistrationDto { Email = email, Password = password, ConfirmPassword = confirmPassword });
        }
    }
}
