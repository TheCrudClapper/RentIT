using RentIT.UI.Contracts.DTO.EquipmentMicroservice.Enums;
using RentIT.UI.Contracts.DTO.EquipmentMicroservice.EquipmentImages;

namespace RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;

public class UserEquipmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public string? InternalNotes { get; set; }
    public Guid CategoryId { get; set; }
    public EquipmentCondition Condition { get; set; }
    public IEnumerable<EquipmentImageResponse> Images { get; set; } = [];
}
