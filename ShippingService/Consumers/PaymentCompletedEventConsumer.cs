using MassTransit;
using Shared.Events;

namespace ShippingService.Consumers
{
    public class PaymentCompletedEventConsumer : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var payment = context.Message;

            // Kargo süreci simülasyonu
            Console.WriteLine($"📦 Kargoya verildi: Sipariş ID: {payment.OrderId}, Müşteri: {payment.CustomerName}, Tutar: {payment.TotalAmount}");
        }
    }
}
