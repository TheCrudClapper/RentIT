using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.DTO.RentalDto
{
    /// <summary>
    /// Represents a request to update the details of a rental, including user, equipment, and rental period
    /// information.
    /// </summary>
    /// <remarks>This class is used to encapsulate the data required to update a rental record.  All required
    /// properties must be provided to ensure the request is valid.</remarks>
    public class RentalUpdateRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
