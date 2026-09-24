using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Domain.Entities.Listing.Errors;

public static class RentalListingErrors
{
    public static readonly Error RentalListingNotFound = Error.Create(ErrorType.NotFound, "RentalListing.NotFound", "RentalListing of given ID not found");

    public static readonly Error RentalListingAlreadyExists = Error.Create(ErrorType.Conflict, "RentalListing.AlreadyExists", "RentalListing with given Title for this Equipment already exists");

    public static readonly Error InvalidRentalDays = Error.Create(ErrorType.Validation, "RentalListing.InvalidRentalDays", "Minimum rental days must be less than or equal to maximum rental days");

    public static readonly Error InvalidQuantity = Error.Create(ErrorType.Validation, "RentalListing.InvalidQuantity", "Quantity must be greater than zero");

    public static readonly Error InvalidPrice = Error.Create(ErrorType.Validation, "RentalListing.InvalidPrice", "Price per day and penalty fee must be greater than zero");
}
