using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using TechChallengeFIAP.Infrastracture.Data;

namespace TechChallengeFIAP.IntegrationTests
{
    public class Application : WebApplicationFactory<TechChallengeFIAPAPI>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<FiapDbContext>));

                services.AddDbContext<FiapDbContext>(options =>
                    options.UseInMemoryDatabase("TesteDatabase", root));
            });

            return base.CreateHost(builder);
        }
    }
}
