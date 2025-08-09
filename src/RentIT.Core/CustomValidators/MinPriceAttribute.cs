using System.ComponentModel.DataAnnotations;
namespace RentIT.Core.CustomValidators
{
    public class MinPriceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not decimal price)
                return new ValidationResult("Price in wrong format");

            if(price < 1)
                return new ValidationResult("Price should be at least 1");

            return ValidationResult.Success;
        }
    }
}
