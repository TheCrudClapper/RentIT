using RentIT.Contracts.DTO.Shared;

namespace RentIT.Contracts.DTO.RentalMicroservice;

public class RentalBrowserItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string CoverImageUrl { get; set; } = null!;
    public CurrencyResponse Money { get; set; } = null!;
    public string Status { get; set; } = null!;
}

