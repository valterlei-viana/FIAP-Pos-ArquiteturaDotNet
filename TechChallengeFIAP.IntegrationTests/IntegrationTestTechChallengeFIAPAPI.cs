using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TechChallengeFIAP.Infrastructure.Data;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestTechChallengeFIAPAPI : WebApplicationFactory<TechChallengeFIAPAPI>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
        }
    }
}
