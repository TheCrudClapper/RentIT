using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.DTO.RentalDto
{
    public class RentalUpdateRequest
    {
        [Required]
        public Guid EquipmentId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
