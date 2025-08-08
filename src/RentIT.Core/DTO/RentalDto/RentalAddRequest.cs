using RentIT.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;
namespace RentIT.Core.DTO.RentalDto
{
    public class RentalAddRequest
    {
        [Required]
        public Guid EquipmentId { get; set; }
        //User ID For testing purposes only
        [Required]
        public Guid UserId { get; set; }
        [Required, FutureDateAttribute, MinDaysBetweenDates("EndDate", 1)]
        public DateTime StartDate { get; set; }
        [Required, FutureDateAttribute]
        public DateTime EndDate { get; set; }
    }
}
