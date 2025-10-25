using ReviewService.Core.DTO.Equipment;

namespace ReviewService.Core.DTO.Rental
{
    public record RentalResponse(Guid Id,
        DateTime? ReturnedDate,
        DateTime StartDate,
        Guid RentedByUserId,
        DateTime EndDate,
        decimal TotalRentalPrice,
        EquipmentResponse equipmentDetails);
}
