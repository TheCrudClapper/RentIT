namespace ReviewService.Core.RabbitMQ.Messages;

public record ReviewUpdated(Guid EquipmentId, double NewRating, double OldRating);
