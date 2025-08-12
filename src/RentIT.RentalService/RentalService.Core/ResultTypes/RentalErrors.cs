namespace RentalService.Core.ResultTypes
{
    public class RentalErrors
    {
        public static readonly Error RentalNotFound = new Error(
            404, "Rental of given Id not found");

        public static readonly Error RentalForSelfEquipment = new Error(
            400, "You can't rent equipment you own yourself !");
    }
}
