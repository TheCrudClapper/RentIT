namespace RentalService.Core.RabbitMQ
{
    public interface IRabbitMQEquipmentDeletedConsumer
    {
        void Consume();
        void Dispose();
    }
}