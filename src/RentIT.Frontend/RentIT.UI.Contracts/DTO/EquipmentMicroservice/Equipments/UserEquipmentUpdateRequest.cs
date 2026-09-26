using RentIT.UI.Contracts.DTO.EquipmentMicroservice.Enums;
using RentIT.UI.Contracts.DTO.EquipmentMicroservice.EquipmentImages;
using System.ComponentModel.DataAnnotations;

namespace RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;

public class UserEquipmentUpdateRequest
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public int Quantity { get; set; }
    [StringLength(maximumLength: 255)]
    public string? InternalNotes { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public EquipmentCondition Condition { get; set; }
    public IEnumerable<EquipmentImageAddRequest> Images { get; set; } = [];
}
