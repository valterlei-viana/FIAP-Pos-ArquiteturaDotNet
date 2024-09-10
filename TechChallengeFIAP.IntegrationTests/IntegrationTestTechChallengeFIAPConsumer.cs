using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestTechChallengeFIAPConsumer : WebApplicationFactory<TechChallengeFIAPConsumer>
    {
        //protected override IHostBuilder CreateHostBuilder()
        //{
        //    return Host.CreateDefaultBuilder()
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            // use whatever config you want here
        //            //webBuilder.UseStartup<Startup>();
        //        });
        //}

        //protected override IHost CreateHost(IHostBuilder builder)
        //{
        //    builder.ConfigureWebHostDefaults(b => b.Configure(app => { }));
        //    return base.CreateHost(builder);
        //}

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.Configure(_ => { });
        //    //builder.UseTestServer();

        //    //base.ConfigureWebHost(builder);
        //}

        //public Task RunHostAsync()
        //{
        //    var host = Services.GetRequiredService<IHost>();
        //    return host.WaitForShutdownAsync();
        //}

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.Configure(_ => { });
        }
    }
}
