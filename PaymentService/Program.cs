using MassTransit;
using PaymentService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// RabbitMQ baðlantýsý ve MassTransit yapýlandýrmasý
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedEventConsumer>(); // Consumer'ý tanýt

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("host.docker.internal", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Önemli: Bu kuyruk tanýmý olmazsa event'ler hiçbir yere gitmez
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

