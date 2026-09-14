using System.ComponentModel.DataAnnotations;
namespace RentIT.UI.Core.DTO.Equipments;

public class EquipmentAddRequest
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public Guid CategoryId { get; set; }
    [Required, StringLength(50)]
    public string SerialNumber { get; set; } = null!;
    [Required]
    public decimal RentalPricePerDay { get; set; }
    [Required]
    public RentStatusEnum Status { get; set; }
    [StringLength(255)]
    public string? Notes { get; set; }
}
