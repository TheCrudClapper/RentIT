using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.Domain.Entities;

public class Category : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(255)]
    public string? Description { get; set; }
    public ICollection<Equipment> EquipmentItems { get; set; } = new List<Equipment>();
}

