using RentalService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalService.Core.Domain.Entities;

/// <summary>
/// Represents a record of an equipment rental, including rental period, associated user, and pricing information.
/// </summary>
/// <remarks>The Rental class tracks the lifecycle of an equipment rental, including creation, editing,
/// and soft deletion states. It includes references to the rented equipment and the user who initiated the rental.
/// Soft deletion is indicated by the IsActive and DateDeleted properties.</remarks>
public class Rental : IBaseEntity, ISoftDelete
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
