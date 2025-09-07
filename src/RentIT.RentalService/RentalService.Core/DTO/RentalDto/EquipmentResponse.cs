namespace RentalService.Core.DTO.RentalDto
{
    public record EquipmentResponse(
        Guid Id,
        string Name,
        Guid CreatedByUserId,
        decimal RentalPricePerDay,
        string SerialNumber,
        string CategoryName,
        string Status,
        string? Notes); 
}
