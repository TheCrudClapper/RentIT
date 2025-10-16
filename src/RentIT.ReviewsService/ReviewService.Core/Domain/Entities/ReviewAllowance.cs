using ReviewServices.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ReviewServices.Core.Domain.Entities;
/// <summary>
/// Entity represents wheter user can write review about certain equipment
/// </summary>
public class ReviewAllowance : IBaseEntity, ISoftDelete
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid EquipmentId { get; set; }

    [Required]
    public Guid RentalId { get; set; }

    public DateTime DateCreated { get; set; }
    public DateTime? DateEdited { get; set; }
    public DateTime? DateDeleted { get; set; }
    public bool IsActive { get; set; }
}
