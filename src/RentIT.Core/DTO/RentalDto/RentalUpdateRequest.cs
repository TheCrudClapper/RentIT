using System.ComponentModel.DataAnnotations;

namespace RentIT.Core.DTO.RentalDto
{
    public class RentalUpdateRequest
    {
        [Required]
        public Guid EquipmentId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal RentalPrice { get; set; }
    }
}
