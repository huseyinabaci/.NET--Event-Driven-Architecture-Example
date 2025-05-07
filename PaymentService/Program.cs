using MassTransit;
using PaymentService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ ba�lant�s� ve MassTransit yap�land�rmas�
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>(); // Consumer'� tan�t

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("host.docker.internal", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // �nemli: Bu kuyruk tan�m� olmazsa event'ler hi�bir yere gitmez
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

