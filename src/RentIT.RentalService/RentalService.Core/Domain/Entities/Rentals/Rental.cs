using RentalService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalService.Core.Domain.Entities.Rentals;

public class Rental : BaseEntity, ISoftDelete
{
    public Guid Id { get; set; }
    public Guid EquipmentId { get; set; }
    public Guid UserId { get; set; }
    public DateTime? ReturnedDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal RentalPrice { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
}
