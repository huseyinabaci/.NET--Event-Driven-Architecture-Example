using MassTransit;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ ba�lant� bilgilerini ve MassTransit yap�land�rmas�n� ekleyelim
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
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
    // Sipari� event'ini RabbitMQ'ya publish edelim
    await bus.Publish(order);
    return Results.Ok($"Order {order.OrderId} created!");
});

app.Run();
