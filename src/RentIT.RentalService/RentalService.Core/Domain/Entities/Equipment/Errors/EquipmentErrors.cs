using RentalService.Core.Domain.ResultTypes;

namespace RentalService.Core.Domain.Entities.Equipment.Errors;

public class EquipmentErrors
{
    public static readonly Error NotOwnerOfEquipment = new(
        ErrorType.Validation, "Equipment.NotOwnerOfEquipment", "You are not the owner of given equipment !");
}
