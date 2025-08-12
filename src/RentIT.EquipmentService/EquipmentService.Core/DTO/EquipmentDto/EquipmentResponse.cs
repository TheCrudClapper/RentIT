namespace EquipmentService.Core.DTO.EquipmentDto;

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public string SerialNumber { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? Notes { get; set; }
}

