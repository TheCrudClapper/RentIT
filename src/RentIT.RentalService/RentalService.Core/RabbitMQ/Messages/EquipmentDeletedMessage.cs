namespace RentalService.Core.RabbitMQ.Messages;

public record EquipmentDeletedMessage(Guid EquipmentId);