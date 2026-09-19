using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.DTO.EquipmentImages;
using System.ComponentModel.DataAnnotations;
namespace EquipmentService.Core.DTO.Equipments.User;

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
