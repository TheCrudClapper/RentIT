using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.Domain.Entities;

public class Category : IBaseEntity, ISoftDelete
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(255)]
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
    public ICollection<Equipment> EquipmentItems { get; set; } = new List<Equipment>();
}

