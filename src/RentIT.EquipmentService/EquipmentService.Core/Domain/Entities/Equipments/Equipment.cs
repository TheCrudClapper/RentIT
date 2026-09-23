using EquipmentService.Core.Domain.Entities.Categories;
using EquipmentService.Core.Domain.Entities.Listing;
using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentService.Core.Domain.Entities.Equipments;

public enum EquipmentCondition
{
    Worn = 1,
    Used = 2,
    New = 3,
}

public class Equipment : BaseEntity, ISoftDelete
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public int Quantity { get; set; }
    [MaxLength(255)]
    public string? InternalNotes { get; set; }
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;
    public EquipmentCondition Condition { get; set; }
    public ICollection<EquipmentImage> Images { get; set; } = new List<EquipmentImage>();
    public ICollection<RentalListing> Listings { get; set; } = new List<RentalListing>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
   
    public void Update(Equipment equipment)
    {
        Name = equipment.Name;
        Description = equipment.Description;
        Quantity = equipment.Quantity;
        InternalNotes = equipment.InternalNotes;
        CategoryId = equipment.CategoryId;
        Images = equipment.Images.ToList();
        DateEdited = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        DateDeleted = DateTime.UtcNow;
    }
}
