using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.DTO.EquipmentImages;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.DTO.Equipments.User;

public class UserEquipmentAddRequest
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
