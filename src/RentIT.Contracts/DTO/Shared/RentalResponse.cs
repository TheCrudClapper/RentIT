using RentIT.Contracts.DTO.RentalMicroservice.Rentals;

namespace RentIT.Contracts.DTO.Shared
{
    public record RentalResponse(Guid Id,
        DateTime? ReturnedDate,
        DateTime StartDate,
        Guid RentedByUserId,
        DateTime EndDate,
        decimal TotalRentalPrice,
        EquipmentResponse EquipmentDetails);
}

