namespace EquipmentService.Core.RabbitMQ.Messages;

public record ReviewCreated(Guid EquipmentId, decimal Rating);