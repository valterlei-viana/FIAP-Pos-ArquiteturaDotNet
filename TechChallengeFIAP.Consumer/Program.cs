using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TechChallengeFIAP.Consumer;
using TechChallengeFIAP.Consumer.Consumers;
using TechChallengeFIAP.Infrastructure.Data;
using TechChallengeFIAP.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var mTservidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var mTusuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var mTsenha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;
var sqlLiteFileName = configuration.GetSection("ConexaoSqlite")["SqlLiteFileName"] ?? string.Empty;
var solutionDir = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent.FullName;
var sqliteConnectionString = "Data Source = " + Path.Combine(solutionDir, sqlLiteFileName);

builder.Services.AddDbContext<FiapDbContext>(opt => opt.UseSqlite(sqliteConnectionString));

ServiceInterfaces.Add(builder.Services);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ContatoInserirConsumer>();
    x.AddConsumer<ContatoAtualizarConsumer>();
    x.AddConsumer<ContatoExcluirConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(mTservidor, "/", h =>
        {
            h.Username(mTusuario);
            h.Password(mTsenha);
        });

        cfg.ReceiveEndpoint("Contato-Inserir", e =>
        {
            e.ConfigureConsumer<ContatoInserirConsumer>(context);
        });

        cfg.ReceiveEndpoint("Contato-Atualizar", e =>
        {
            e.ConfigureConsumer<ContatoAtualizarConsumer>(context);
        });

        cfg.ReceiveEndpoint("Contato-Excluir", e =>
        {
            e.ConfigureConsumer<ContatoExcluirConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();

//Necessario para expor a class Program
public partial class TechChallengeFIAPConsumer { }