using RentIT.Contracts.DTO.EquipmentMicroservice.Images;
using RentIT.Contracts.DTO.Shared;

namespace RentIT.Contracts.DTO.EquipmentMicroservice.RentalListings;

public class UserRentalListingListItem
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Quantity { get; set; }
    public CurrencyResponse PricePerDay { get; set; } = null!;
    public int ListingStatus { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateEdited { get; set; }
    public ImageResponse Thumbnail { get; set; } = null!;
}

