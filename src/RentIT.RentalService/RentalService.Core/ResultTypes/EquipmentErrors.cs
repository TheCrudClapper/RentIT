namespace RentalService.Core.ResultTypes;

public class EquipmentErrors
{
    public static readonly Error NotOwnerOfEquipment = new Error(
        400, "You are not the owner of given equipment !");
}
