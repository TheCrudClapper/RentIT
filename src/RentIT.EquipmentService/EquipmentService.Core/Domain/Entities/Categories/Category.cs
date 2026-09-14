using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.Domain.Entities.Categories;

public class Category : BaseEntity, ISoftDelete
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(255)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
    public ICollection<Equipment> EquipmentItems { get; set; } = new List<Equipment>();
}

