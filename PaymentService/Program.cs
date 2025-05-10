using MassTransit;
using PaymentService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ bağlantısı ve MassTransit yapılandırması
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>(); // Consumer'ı tanıt

    x.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        
        cfg.Host(host, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Önemli: Bu kuyruk tanımı olmazsa event'ler hiçbir yere gitmez
        cfg.ReceiveEndpoint("order-created-event-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

