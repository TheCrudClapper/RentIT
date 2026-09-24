using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Entities.Shared;
using EquipmentService.Core.DTO.RentalListings;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.Mappings;

public static class RentalListingMappings
{
    public static RentalListingResponse ToRentalListingResponse(this RentalListing rentalListing)
    {
        return new RentalListingResponse
        {
            Id = rentalListing.Id,
            EquipmentId = rentalListing.EquipmentId,
            Title = rentalListing.Title,
            Description = rentalListing.Description,
            Quantity = rentalListing.Quantity,
            PricePerDay = rentalListing.PricePerDay.ToCurrencyResponse(),
            PenaltyFeePerDay = rentalListing.PenaltyFeePerDay.ToCurrencyResponse(),
            MaximumRentalDays = rentalListing.MaximumRentalDays,
            MinimumRentalDays = rentalListing.MinimumRentalDays,
            ListingStatus = (int)rentalListing.ListingStatus,
            DateCreated = rentalListing.DateCreated,
            DateEdited = rentalListing.DateEdited ?? DateTime.UtcNow
        };
    }

    public static RentalListingListResponse ToRentalListingListResponse(this RentalListing rentalListing)
    {
        return new RentalListingListResponse
        {
            Id = rentalListing.Id,
            Title = rentalListing.Title,
            Quantity = rentalListing.Quantity,
            PricePerDay = rentalListing.PricePerDay.ToCurrencyResponse(),
            PenaltyFeePerDay = rentalListing.PenaltyFeePerDay.ToCurrencyResponse(),
            MaximumRentalDays = rentalListing.MaximumRentalDays,
            MinimumRentalDays = rentalListing.MinimumRentalDays,
            ListingStatus = (int)rentalListing.ListingStatus,
            DateCreated = rentalListing.DateCreated,
            DateEdited = rentalListing.DateEdited ?? DateTime.UtcNow
        };
    }

    public static UserRentalListingListResponse ToUserRentalListingListResponse(this RentalListing rentalListing)
    {
        return new UserRentalListingListResponse
        {
            Id = rentalListing.Id,
            EquipmentId = rentalListing.EquipmentId,
            Title = rentalListing.Title,
            Description = rentalListing.Description,
            Quantity = rentalListing.Quantity,
            PricePerDay = rentalListing.PricePerDay.ToCurrencyResponse(),
            ListingStatus = (int)rentalListing.ListingStatus,
            DateCreated = rentalListing.DateCreated,
            DateEdited = rentalListing.DateEdited ?? DateTime.UtcNow
        };
    }

    public static RentalListing ToRentalListing(this RentalListingAddRequest request)
    {
        return new RentalListing
        {
            Id = Guid.NewGuid(),
            EquipmentId = request.EquipmentId,
            Title = request.Title,
            Description = request.Description,
            Quantity = request.Quantity,
            PricePerDay = request.PricePerDay.ToCurrency(),
            PenaltyFeePerDay = request.PenaltyFeePerDay.ToCurrency(),
            MaximumRentalDays = request.MaximumRentalDays,
            MinimumRentalDays = request.MinimumRentalDays,
            ListingStatus = ListingStatus.Open,
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateEdited = DateTime.UtcNow
        };
    }

    public static RentalListing ToRentalListing(this RentalListingUpdateRequest request)
    {
        return new RentalListing
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Quantity = request.Quantity,
            PricePerDay = request.PricePerDay.ToCurrency(),
            PenaltyFeePerDay = request.PenaltyFeePerDay.ToCurrency(),
            MaximumRentalDays = request.MaximumRentalDays,
            MinimumRentalDays = request.MinimumRentalDays,
            ListingStatus = ListingStatus.Open,
            IsActive = true,
            DateCreated = DateTime.UtcNow,
            DateEdited = DateTime.UtcNow
        };
    }

    public static CreatedResponse ToCreatedResponse(this RentalListing rentalListing)
    {
        return new CreatedResponse(
            rentalListing.Id,
            rentalListing.DateCreated
        );
    }

    public static UpdatedResponse ToUpdatedResponse(this RentalListing rentalListing)
    {
        return new UpdatedResponse(
            rentalListing.Id,
            rentalListing.DateEdited ?? DateTime.UtcNow
        );
    }

    private static CurrencyResponse ToCurrencyResponse(this Currency currency)
    {
        return new CurrencyResponse
        {
            CurrencyCode = currency.CurrencyCode,
            Amount = currency.Amount
        };
    }
}
