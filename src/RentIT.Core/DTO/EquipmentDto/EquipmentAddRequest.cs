using RentIT.Core.CustomValidators;
using RentIT.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentIT.Core.DTO.EquipmentDto
{
    public class EquipmentAddRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        //User ID For testing purposes only
        [Required]
        public Guid UserId { get; set; }
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
