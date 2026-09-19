using EquipmentService.Core.Domain.Interfaces;

namespace EquipmentService.Core.Domain.Entities.Equipments;

public class EquipmentImage : BaseEntity, ISoftDelete
{
    public string ResourcePath { get; set; } = null!;
    public Guid EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
