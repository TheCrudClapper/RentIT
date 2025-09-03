namespace EquipmentService.Core.ResultTypes;

public class UserErrors
{
    public static readonly Error UserNotFound = new Error(
        404, "User of given Id not found");
}

