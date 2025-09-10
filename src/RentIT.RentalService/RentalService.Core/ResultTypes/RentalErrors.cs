namespace RentalService.Core.ResultTypes
{
    public class RentalErrors
    {
        public static readonly Error RentalNotFound = new Error(
            404, "Rental of given Id not found");

        public static readonly Error RentalForSelfEquipment = new Error(
            400, "You can't rent equipment you own yourself !");

        public static readonly Error RentalPeriodNotAvaliable = new Error(
            400, "Equipment is already rented during the requested period.");

        public static readonly Error FailedToDeleteRelatedRentals = new Error(
            400, "Failed to delete related rentals, try again later.");
    }
}
