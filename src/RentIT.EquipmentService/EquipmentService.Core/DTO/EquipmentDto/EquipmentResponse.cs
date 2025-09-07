namespace EquipmentService.Core.DTO.EquipmentDto;

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CreatedByUserId { get; set; }
    public string SerialNumber { get; set; } = null!;
    public decimal RentalPricePerDay { get; set; }
    public string CategoryName { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? Notes { get; set; }
}

