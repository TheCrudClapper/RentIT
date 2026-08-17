using RentalService.Core.Domain.ResultTypes;

namespace RentalService.Core.Domain.Entities.Errors;

public class RentalErrors
{
    public static readonly Error NotFound = new Error(
        ErrorType.NotFound, "Rental.NotFound", "Rental of given Id not found");

    public static readonly Error RentalForSelfEquipment = new Error(
        ErrorType.Validation, "Rental.RentalForSelfEquipment", "You can't rent equipment you own yourself !");

    public static readonly Error RentalPeriodNotAvaliable = new Error(
        ErrorType.Conflict, "Rental.RentalPeriodNotAvaliable", "Equipment is already rented during the requested period.");

    public static readonly Error FailedToDeleteRelatedRentals = new Error(
        ErrorType.Validation, "Rental.FailedToDeleteRelatedRentals", "Failed to delete related rentals, try again later.");

    public static readonly Error InvalidReturnedDate = new Error(
        ErrorType.Validation, "Rental.InvalidReturnedDate", "Returned date must be after rental start date");
}
