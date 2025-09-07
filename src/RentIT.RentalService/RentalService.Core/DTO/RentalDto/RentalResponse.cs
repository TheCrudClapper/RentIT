namespace RentalService.Core.DTO.RentalDto
{
    public record RentalResponse(Guid Id,
        DateTime? ReturnedDate,
        DateTime StartDate,
        DateTime EndDate,
        decimal TotalRentalPrice,
        EquipmentResponse equipmentDetails);
}
