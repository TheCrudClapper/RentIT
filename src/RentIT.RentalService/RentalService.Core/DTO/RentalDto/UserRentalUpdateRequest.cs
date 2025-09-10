using RentalService.Core.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.DTO.RentalDto
{
    /// <summary>
    /// Represents a request to update the end date of a user's rental.
    /// </summary>
    /// <remarks>The <see cref="EndDate"/> must be a future date and is required for the update to be
    /// valid.</remarks>
    public class UserRentalUpdateRequest
    {
        [Required, FutureDateAttribute, MinDaysBetweenDates("EndDate", 1)]
        public DateTime StartDate { get; set; }

        [Required, FutureDateAttribute]
        public DateTime EndDate;
    }
}
