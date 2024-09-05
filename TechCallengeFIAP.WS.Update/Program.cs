using TechCallengeFIAP.WS.Update;
using MassTransit;
using TechChallengeFIAP.Core.Entities;
using static TechChallengeFIAP.Core.Entities.Contato;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        var nomefila = configuration.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
        var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
        var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
        var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;
        services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(servidor, "/", h =>
                {
                    h.Username(usuario);
                    h.Password(senha);
                });

                //cfg.ReceiveEndpoint(nomefila, e =>
                //{
                //    e.Consumer<PedidoCriadoConsumidor>(context);
                //});

                cfg.ConfigureEndpoints(context);
            });

            //x.AddConsumer<PedidoCriadoConsumidor>();
        });
    })
    .Build();

host.Run();