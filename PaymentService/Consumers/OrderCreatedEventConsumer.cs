using MassTransit;
using Shared.Events;

namespace PaymentService.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var order = context.Message;

            Console.WriteLine($"✅ Ödeme alındı: Sipariş ID: {order.OrderId}, Müşteri: {order.CustomerName}, Tutar: {order.TotalAmount}");

            // Yeni: Ödeme tamamlandı event'ini publish et
            var paymentCompletedEvent = new PaymentCompletedEvent
            {
                OrderId = order.OrderId,
                CustomerName = order.CustomerName,
                TotalAmount = order.TotalAmount
            };

            await context.Publish(paymentCompletedEvent);
        }
    }
}
