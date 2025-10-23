using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.RabbitMQ.Messages;

public record ReviewAllowanceAddRequest
{
    [Required]
    public Guid UserId { get; init; }

    [Required]
    public Guid EquipmentId { get; init; }

    [Required]
    public Guid RentalId { get; init; }
}