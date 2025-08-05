namespace RentIT.Core.ResultTypes
{
    public class EquipmentErrors
    {
        public static readonly Error EquipmentNotFound = new Error(
            404, "Equipment of given Id not found");
    }
}
