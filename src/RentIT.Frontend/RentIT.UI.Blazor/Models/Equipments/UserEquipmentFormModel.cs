using RentIT.UI.Contracts.DTO.EquipmentMicroservice.Enums;
using System.ComponentModel.DataAnnotations;

namespace RentIT.BlazorFrontend.Models.Equipments;

public class UserEquipmentFormModel
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
    public IEnumerable<EquipmentImage> Images { get; set; } = [];
}
