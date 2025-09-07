using RentalService.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.DTO.RentalDto
{
    /// <summary>
    /// Represents a request to add a new rental for a user, specifying the equipment and rental period.
    /// </summary>
    /// <remarks>The rental period must start and end on future dates, with at least one day between the start
    /// and end dates.</remarks>
    public record UserRentalAddRequest()
    {
        [Required]
        public Guid EquipmentId;

        [Required, FutureDateAttribute, MinDaysBetweenDates("EndDate", 1)]
        public DateTime StartDate;

        [Required, FutureDateAttribute]
        public DateTime EndDate;
    }
}
