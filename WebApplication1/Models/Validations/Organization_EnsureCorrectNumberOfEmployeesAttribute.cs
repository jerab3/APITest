using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Validations
{
    public class Organization_EnsureCorrectNumberOfEmployeesAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var organization = validationContext.ObjectInstance as Organization;
            if (organization != null && organization.NumberOfEmployees == 0)
            {
                return new ValidationResult("The number of employees must be greater than 0");
            }
            return ValidationResult.Success;
        }
    }
}
