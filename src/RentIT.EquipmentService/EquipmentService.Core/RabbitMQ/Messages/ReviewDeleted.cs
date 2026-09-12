namespace EquipmentService.Core.RabbitMQ.Messages;

public record ReviewDeleted(Guid EquipmentId, decimal Rating);
