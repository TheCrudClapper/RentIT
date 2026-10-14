using EquipmentService.Core.Domain.Entities.Shared;
using RentIT.Contracts.DTO.EquipmentMicroservice.RentalListings;

namespace EquipmentService.Core.Mappings;

public static class CurrencyMappings
{
    public static Currency ToCurrency(this CurrencyRequest currencyRequest)
    {
        return new Currency
        {
            CurrencyCode = currencyRequest.CurrencyCode,
            Amount = currencyRequest.Amount
        };
    }
}
