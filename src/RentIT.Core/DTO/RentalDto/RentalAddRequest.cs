using RentIT.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;
namespace RentIT.Core.DTO.RentalDto
{
    public class RentalAddRequest
    {
        [Required]
        public Guid EquipmentId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required, FutureDateValidator]
        public DateTime StartDate { get; set; }
        [Required, FutureDateValidator]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal RentalPrice { get; set; }
    }
}
