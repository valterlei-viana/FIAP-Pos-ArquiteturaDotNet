using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestTechChallengeFIAPConsumer : WebApplicationFactory<TechChallengeFIAPConsumer>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.Configure(_ => { });
        }

        public Task RunHostAsync()
        {
            var host = Services.GetRequiredService<IHost>();
            return host.WaitForShutdownAsync();
        }
    }
}