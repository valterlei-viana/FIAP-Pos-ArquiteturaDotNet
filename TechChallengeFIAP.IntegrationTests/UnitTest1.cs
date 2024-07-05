using System.Net;
using System.Net.Http.Json;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.IntegrationTests
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public async Task Test1()
        {
            await using var app = new Application();

            var url = "cadastro/find/1";

            var client = app.CreateClient();
            var result = await client.GetAsync(url);
            var contato = await client.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(contato);
            Assert.IsTrue(contato.Id == 1);
        }
    }
}