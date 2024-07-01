using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Repositories;
using TechChallengeFIAP.Infrastructure.Services;

namespace TechChallengeFIAP.API.Middleware;

public static class ServiceInterfaces
{
    public static void Add(IServiceCollection pServices)
    {
        pServices.AddTransient<IDDDRegionService, DDDRegionService>();
        pServices.AddTransient<IContatoRepository, ContatoRepository>();
    }
}

