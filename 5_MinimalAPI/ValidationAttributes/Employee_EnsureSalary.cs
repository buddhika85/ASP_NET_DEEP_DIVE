using MinimalAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RoutingExcercise.ValidationAttributes
{
    public class Employee_EnsureSalary : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var employee = validationContext.ObjectInstance as Employee;
            if (employee != null && 
                employee.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase)
                && employee.Salary <= 100000) 
            {
                return new ValidationResult("A manager salary should be greater than $100,000");
            }
            return ValidationResult.Success;
        }
    }
}
