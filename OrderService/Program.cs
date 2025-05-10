using MassTransit;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ bağlantı bilgilerini ve MassTransit yapılandırmasını ekleyelim
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
        
        cfg.Host(host, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/create-order", async (IBus bus, OrderCreatedEvent order) =>
{
    // Sipariş event'ini RabbitMQ'ya publish edelim
    await bus.Publish(order);
    return Results.Ok($"Order {order.OrderId} created!");
});

app.Run();
