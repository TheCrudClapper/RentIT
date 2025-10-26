namespace ReviewService.Core.RabbitMQ.Messages;

public record ReviewUpdated(Guid EquipmentId, decimal NewRating, decimal OldRating);
