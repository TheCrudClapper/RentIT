using System.ComponentModel.DataAnnotations;

namespace RentIT.Core.CustomValidators
{
    public class FutureDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if(value != null)
            {
                DateTime date;
                bool parseResult = DateTime.TryParse(value.ToString(), out date);

                if (!parseResult)
                    return new ValidationResult("Error while parsing date");

                if(date < DateTime.UtcNow)
                {
                    return new ValidationResult("Given date cannot be in past");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
