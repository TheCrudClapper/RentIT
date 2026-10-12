using RentIT.Contracts.DTO.EquipmentMicroservice.Enums;
using RentIT.Contracts.DTO.EquipmentMicroservice.Images;
using RentIT.Contracts.Validation;
using System.ComponentModel.DataAnnotations;

namespace RentIT.Contracts.DTO.EquipmentMicroservice.Equipments.Admin;

public class EquipmentAddRequestInternal
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
    public IEnumerable<ImageRequest> Images { get; set; } = [];
}



