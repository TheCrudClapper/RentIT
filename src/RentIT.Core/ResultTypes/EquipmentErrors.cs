using RentIT.Core.Domain.Entities;

namespace RentIT.Core.ResultTypes
{
    public class EquipmentErrors
    {
        public static readonly Error EquipmentNotFound = new Error(
            404, "Equipment of given Id not found");

        public static Error EquipmentNotAvaliable = new Error(
           422, $"Equipment is not avaliable right now");
    }
}
