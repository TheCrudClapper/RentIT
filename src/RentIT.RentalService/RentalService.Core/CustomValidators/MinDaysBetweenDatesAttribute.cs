using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.CustomValidators
{
    public class MinDaysBetweenDatesAttribute : ValidationAttribute
    {
        private readonly int _minDaysBetweenDates;
        private readonly string _otherPropertyName;
        public MinDaysBetweenDatesAttribute(string otherPropertyName, int minDaysBetweenDates)
        {
            _minDaysBetweenDates = minDaysBetweenDates;
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentDate = value as DateTime?;
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherProperty == null)
                return new ValidationResult($"Property {_otherPropertyName} not found");

            var otherDate = otherProperty.GetValue(validationContext.ObjectInstance) as DateTime?;

            if (currentDate.HasValue && otherDate.HasValue)
            {
                var differrence = (otherDate.Value - currentDate.Value).TotalDays;

                if (differrence < _minDaysBetweenDates)
                    return new ValidationResult(
                    $"The difference between {validationContext.DisplayName} and {_otherPropertyName} must be at least {_minDaysBetweenDates} day/s."
                );
            }

            return ValidationResult.Success;

        }
    }
}
