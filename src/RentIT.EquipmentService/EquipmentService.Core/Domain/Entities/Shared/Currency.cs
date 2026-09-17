namespace EquipmentService.Core.Domain.Entities.Shared;

public class Currency
{
    public string CurrencyCode { get; set; } = null!;
    public decimal Amount { get; set; }
}
