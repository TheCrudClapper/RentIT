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
        [Required, FutureDateAttribute]
        public DateTime StartDate { get; set; }
        [Required, FutureDateAttribute]
        public DateTime EndDate { get; set; }
    }
}
