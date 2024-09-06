using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Repositories;
using TechChallengeFIAP.Infrastructure.Services;

namespace TechChallengeFIAP.Infrastructure.Middleware;

public static class ServiceInterfaces
{
    public static void Add(IServiceCollection pServices)
    {
        pServices.AddHttpClient();
        pServices.AddTransient<IDDDRegionService, DDDRegionService>();
        pServices.AddTransient<IContatoRepository, ContatoRepository>();
    }
}

