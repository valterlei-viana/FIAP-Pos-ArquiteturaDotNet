using MassTransit;
using TechChallenge.Consumer;
using TechChallenge.Consumer.Events;

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;
var fila = configuration.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;


builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ReceiveEndpoint(fila, e =>
        {
            e.Consumer<ContatoUpdateConsumidor>();
        });

        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<ContatoUpdateConsumidor>();
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();