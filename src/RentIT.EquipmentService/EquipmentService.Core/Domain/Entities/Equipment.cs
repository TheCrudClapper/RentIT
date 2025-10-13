using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentService.Core.Domain.Entities;

/// <summary>
/// Represents the status of a rental item.
/// </summary>
/// <remarks>This enumeration is used to indicate the current availability or condition of a rental item.
/// Possible values include: <list type="bullet"> <item><description><see cref="Avaliable"/>: The item is available
/// for rent.</description></item> <item><description><see cref="Rented"/>: The item is currently
/// rented.</description></item> <item><description><see cref="Maintenance"/>: The item is undergoing maintenance
/// and is not available for rent.</description></item> </list></remarks>
public enum RentStatusEnum
{
    Avaliable = 1,
    Rented = 2,
    Maintenance = 3
}
/// <summary>
/// Represents a piece of equipment available for rental or inventory tracking.
/// </summary>
/// <remarks>The <see cref="Equipment"/> class provides properties to store details about the equipment, 
/// including its name, category, serial number, rental status, and optional notes.  It is associated with a
/// specific category through the <see cref="CategoryId"/> property.</remarks>
public class Equipment : IBaseEntity, ISoftDelete
{
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public Guid CreatedByUserId { get; set; }
    public Guid CategoryId { get; set; }

    [MaxLength(50)]
    public string SerialNumber { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal RentalPricePerDay { get; set; }

    public RentStatusEnum Status { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; } = null!;

    public int ReviewCount { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal AverageRating { get; set; }

    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
}

