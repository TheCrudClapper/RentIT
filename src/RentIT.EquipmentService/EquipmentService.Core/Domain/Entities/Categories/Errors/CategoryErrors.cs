using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Domain.Entities.Categories.Errors;

public static class CategoryErrors
{
    public static readonly Error CategoryNotFound = Error.Create(ErrorType.NotFound, "Category.NotFound", "Category of given ID not found");

    public static readonly Error CategoryAlreadyExists = Error.Create(ErrorType.Conflict, "Category.AlreadyExists", "Category of given Name already exists, please correct name");
}
