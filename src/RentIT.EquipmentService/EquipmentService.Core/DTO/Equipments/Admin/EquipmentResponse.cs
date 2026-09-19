using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.DTO.EquipmentImages;

namespace EquipmentService.Core.DTO.Equipments.Admin;

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public string? InternalNotes { get; set; }
    public Guid CategoryId { get; set; }
    public EquipmentCondition Condition { get; set; }
    public IEnumerable<EquipmentImageResponse> Images { get; set; } = [];
}

