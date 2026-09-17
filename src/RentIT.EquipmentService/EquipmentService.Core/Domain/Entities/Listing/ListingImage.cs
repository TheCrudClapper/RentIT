namespace EquipmentService.Core.Domain.Entities.Listing;
public class ListingImage
{
    public string ResourcePath { get; set; } = null!;
    public Guid ListingId { get; set; }
    public RentalListing RentalListing { get; set; } = null!;
}
