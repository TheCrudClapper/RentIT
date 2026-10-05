using EquipmentService.API.DTO.Public;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.Attributes;

public class OneCoverAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var images = validationContext.ObjectInstance as IEnumerable<ImageRequestExternal>;
        if (images is null || images.Count() == 0)
            return new ValidationResult("You need to upload at least one image");

        int covers = images.Where(x => x.IsCover).Count();
        if (covers < 0)
            return new ValidationResult("At least one image needs to be marked as cover.");
        else if (covers > 1)
            return new ValidationResult("There can be only one cover image.");
        else 
            return ValidationResult.Success;

    }
}
