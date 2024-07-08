using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestWebApplicationFactory : WebApplicationFactory<TechChallengeFIAPAPI>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            base.ConfigureWebHost(builder);
        }
    }
}
