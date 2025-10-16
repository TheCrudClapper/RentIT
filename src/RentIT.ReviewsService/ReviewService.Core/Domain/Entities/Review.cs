using ReviewServices.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
namespace ReviewServices.Core.Domain.Entities;

/// <summary>
/// Entity that represents Review issued after successfull return of rented item
/// </summary>
public class Review : IBaseEntity, ISoftDelete
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId {  get; set; }

    [Required]
    public Guid EquipmentId { get; set; }

    [Required]
    public Guid RentalId { get; set; }

    [Required]
    [MaxLength(1024)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(1, 5)]
    public double Rating { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public DateTime? DateDeleted { get; set; }
    public bool IsActive { get; set; }
}
