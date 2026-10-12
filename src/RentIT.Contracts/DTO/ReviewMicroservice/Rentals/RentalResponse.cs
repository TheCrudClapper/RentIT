using RentIT.Contracts.DTO.ReviewMicroservice.Equipment;

namespace RentIT.Contracts.DTO.ReviewMicroservice.Rentals
{
    public record RentalResponse(Guid Id,
        DateTime? ReturnedDate,
        DateTime StartDate,
        Guid RentedByUserId,
        DateTime EndDate,
        decimal TotalRentalPrice,
        EquipmentResponse EquipmentDetails);
}

