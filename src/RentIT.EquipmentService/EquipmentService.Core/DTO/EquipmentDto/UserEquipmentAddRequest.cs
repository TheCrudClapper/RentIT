using EquipmentService.Core.CustomValidators;
using EquipmentService.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
namespace EquipmentService.Core.DTO.EquipmentDto
{
    public class UserEquipmentAddRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Guid CategoryId { get; set; }

        [Required, StringLength(50)]
        public string SerialNumber { get; set; } = null!;

        [Required, MinPrice]
        public decimal RentalPricePerDay { get; set; }

        [Required]
        public RentStatusEnum Status { get; set; }

        [StringLength(255)]
        public string? Notes { get; set; }
    }
}
