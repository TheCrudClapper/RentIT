using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.CustomValidators
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute()
        {
            ErrorMessage = "The date must be in the future.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not DateTime date)
                return new ValidationResult("Invalid date format.");

            date = date.Date;

            if (date.Date <= DateTime.UtcNow.Date)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
