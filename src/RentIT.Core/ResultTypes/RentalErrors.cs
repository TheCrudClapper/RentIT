namespace RentIT.Core.ResultTypes
{
    public class RentalErrors
    {
        public static readonly Error RentalNotFound = new Error(
            404, "Rental of given Id not found");
    }
}
