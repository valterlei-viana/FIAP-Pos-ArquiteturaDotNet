using MassTransit;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Consumer;
using TechChallenge.Consumer.Events;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastracture.Data;
using TechChallengeFIAP.Infrastructure.Middleware;
using TechChallengeFIAP.Infrastructure.Repositories;

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;
var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

builder.Services.AddDbContext<FiapDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "fiap"));

ServiceInterfaces.Add(builder.Services);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContatoAtualizarConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ReceiveEndpoint("Contato-Atualizar", e =>
        {
            e.ConfigureConsumer<ContatoAtualizarConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();