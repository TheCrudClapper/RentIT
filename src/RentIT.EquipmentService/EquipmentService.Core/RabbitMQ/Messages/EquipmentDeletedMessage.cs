namespace EquipmentService.Core.RabbitMQ.Messages;

public record EquipmentDeletedMessage(Guid EquipmentId);