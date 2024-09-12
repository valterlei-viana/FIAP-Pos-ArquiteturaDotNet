using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TechChallengeFIAP.IntegrationTests
{
    public class IntegrationTestTechChallengeFIAPConsumer : WebApplicationFactory<TechChallengeFIAPConsumer>
    {
    }
}