using RentalService.Core.Attributes;
using System.ComponentModel.DataAnnotations;
namespace RentalService.Core.DTO.Rentals;

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
