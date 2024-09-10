using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechChallengeFIAP.Infrastructure.Data;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestTechChallengeFIAPAPI : WebApplicationFactory<TechChallengeFIAPAPI>
    {
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices(services =>
        //    {
        //        //services.AddDbContext<FiapDbContext>(opt => opt.UseSqlite(sqliteConnectionString));
        //    });
        //}
    }
}
