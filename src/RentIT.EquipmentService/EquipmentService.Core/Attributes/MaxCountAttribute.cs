using EquipmentService.Core.DTO.Images;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.Attributes;

public class MaxCountAttribute : ValidationAttribute
{
    public int MaxCount { get; set; }
    private string _defaultErrorMessage =>
        $"You can upload up to {MaxCount} images per equipment";
    public MaxCountAttribute(int maxCount)
    {
        MaxCount = maxCount;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var list = validationContext.ObjectInstance as IEnumerable<ImageRequest>;
        if (list is null)
            return new ValidationResult("List cannot be null");

        if(list.Count() > MaxCount)
            return new ValidationResult(ErrorMessage ?? _defaultErrorMessage);

        return ValidationResult.Success;
    }
}
