namespace RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;

public class EquipmentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public string SerialNumber { get; set; } = null!;
    public decimal RentalPricePerDay { get; set; }
    public RentStatusEnum Status { get; set; }
    public string? Notes { get; set; }
}
