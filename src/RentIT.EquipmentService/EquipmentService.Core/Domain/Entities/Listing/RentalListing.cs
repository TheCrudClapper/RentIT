using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Entities.Shared;
using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentService.Core.Domain.Entities.Listing;
public enum ListingStatus
{
    Open = 1,
    Closed = 2
}

public class RentalListing : BaseEntity, ISoftDelete
{
    public int EquipmentId { get; set; }
    [ForeignKey("EquipmentId")]
    public Equipment Equipment { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public Currency PricePerDay { get; set; } = null!;
    public Currency PenaltyFeePerDay { get; set; } = null!;
    public ListingStatus ListingStatus { get; set; }
    public int MaximumRentalDays { get; set; }
    public int MinimumRentalDays { get; set; }
    public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
