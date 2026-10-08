using RentIT.BlazorFrontend.Models.Shared;
using RentIT.UI.Contracts.DTO.Shared;

namespace RentIT.BlazorFrontend.Models.Rentals
{
    public class RentalBrowserItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public CurrencyViewModel Money { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
