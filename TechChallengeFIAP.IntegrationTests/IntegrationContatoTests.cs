
using System.Net;
using System.Net.Http.Json;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.IntegrationTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private IntegrationTestWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup() => _factory = new IntegrationTestWebApplicationFactory();

        [SetUp]
        public void Setup() => _client = _factory.CreateClient();

        public void Dispose() => _factory?.Dispose();

        [Test, Order(1)]
        public async Task Inserir_Contato()
        {
            var url = "/Inserir/Contato";

            var contato = new Contato()
            {
                Email = "valterlei.viana@gmail.com",
                Nome = "Valterlei",
                Telefone = new Telefone()
                {
                    DDD = "11",
                    Numero = "994870098"
                }
            };

            var result = await _client.PostAsJsonAsync(url, contato);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }


        [Test, Order(2)]
        public async Task Buscar_DDD()
        {
            var url = "/Buscar/DDD?DDD=11";

            var result = await _client.GetAsync(url);
            var contato = await _client.GetFromJsonAsync<IEnumerable<Contato>>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(contato);
            Assert.IsTrue(contato.First().Email == "valterlei.viana@gmail.com");
        }

        [Test, Order(3)]
        public async Task Buscar_Nome()
        {
            var url = $"/Buscar/Nome?nome=Valterlei";

            var result = await _client.GetAsync(url);
            var contato = await _client.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(contato);
            Assert.IsTrue(contato.Email == "valterlei.viana@gmail.com");
        }

        [Test, Order(7)]
        public async Task Buscar_Id()
        {
            var url = "/Buscar/Id?id=1";

            var result = await _client.GetAsync(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test, Order(5)]
        public async Task Atualizar_Contato()
        {
            var url = "/Atualizar/Contato?id=1";

            var contato = new Contato()
            {
                Email = "valterlei.viana@gmail.com",
                Nome = "Valterlei - Atualizado",
                Telefone = new Telefone()
                {
                    DDD = "11",
                    Numero = "994870098"
                }
            };

            var result = await _client.PutAsJsonAsync(url, contato);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            url = $"/Buscar/Nome?nome=" + "Valterlei - Atualizado";

            result = await _client.GetAsync(url);
            contato = await _client.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsNotNull(contato);
            Assert.IsTrue(contato.Email == "valterlei.viana@gmail.com");
        }

        [Test, Order(6)]
        public async Task Excluir_Contato()
        {
            var url = "/Deletar/Contato?id=1";

            var result = await _client.DeleteAsync(url);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}