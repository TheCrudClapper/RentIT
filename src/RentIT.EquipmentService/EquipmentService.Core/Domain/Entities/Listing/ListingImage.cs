using EquipmentService.Core.Domain.Interfaces;

namespace EquipmentService.Core.Domain.Entities.Listing;
public class ListingImage : BaseEntity, ISoftDelete
{
    public string ResourcePath { get; set; } = null!;
    public Guid ListingId { get; set; }
    public RentalListing RentalListing { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        DateDeleted = DateTime.UtcNow;
        IsActive = false;
    }
}
