namespace EquipmentService.Core.Domain.Entities.Equipments;

public class EquipmentImage
{
    public string ResourcePath { get; set; } = null!;
    public Guid EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
}
