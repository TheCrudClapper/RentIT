using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Domain.Entities.User.Errors;

public class UserErrors
{
    public static readonly Error UserNotFound = Error.Create(ErrorType.NotFound, "User.NotFound", "User of given Id not found");
}
