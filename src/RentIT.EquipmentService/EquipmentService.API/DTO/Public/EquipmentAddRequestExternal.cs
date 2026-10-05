using EquipmentService.Core.Attributes;
using EquipmentService.Core.Domain.Entities.Equipments;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.API.DTO.Public;

public class EquipmentAddRequestExternal
{

    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [StringLength(maximumLength: 255)]
    public string? InternalNotes { get; set; }
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public EquipmentCondition Condition { get; set; }

    [Required]
    [MaxCount(10, ErrorMessage = $"You can upload up to 10 images per equipment.")]
    public IEnumerable<ImageRequestExternal> Images { get; set; } = [];
}

