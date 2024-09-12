using NUnit.Framework.Legacy;
using System.Net;
using System.Net.Http.Json;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.IntegrationTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private IntegrationTestTechChallengeFIAPAPI FIAPAPI;
        private IntegrationTestTechChallengeFIAPConsumer FIAPConsumer;
        private HttpClient clientAPI, clientConsumer;
        private int id;
        private string guid = Guid.NewGuid().ToString();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            FIAPAPI = new IntegrationTestTechChallengeFIAPAPI();
            FIAPConsumer = new IntegrationTestTechChallengeFIAPConsumer();
            clientConsumer = FIAPConsumer.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
            clientAPI = FIAPAPI.CreateClient();
        }

        [TearDown]
        public async Task Teardown()
        {
            clientConsumer = FIAPConsumer.CreateClient();
            await Task.Delay(1000);
        }

        public void Dispose()
        {
            FIAPConsumer?.Dispose();
            FIAPAPI?.Dispose();
            clientAPI?.Dispose();
            clientConsumer?.Dispose();
        }

        [Test, Order(1)]
        public async Task Inserir_Contato()
        {
            var url = "/Contato/Inserir";

            var contato = new Contato()
            {
                Email = $"{guid}@gmail.com",
                Nome = guid,
                Telefone = new Telefone()
                {
                    DDD = "11",
                    Numero = "955778899",
                    UF = "SP"
                }
            };

            var result = await clientAPI.PostAsJsonAsync(url, contato);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));            
        }

        [Test, Order(2)]
        public async Task Buscar_DDD()
        {
            var url = "/Contato/Buscar/DDD?DDD=11";

            var result = await clientAPI.GetAsync(url);
            var contatos = await clientAPI.GetFromJsonAsync<IEnumerable<Contato>>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contatos);
            ClassicAssert.IsTrue(contatos.Where(x => x.Nome == guid).Single() != null);
        }

        [Test, Order(3)]
        public async Task Buscar_Nome()
        {
            var url = $"/Contato/Buscar/Nome?nome={guid}";

            var result = await clientAPI.GetAsync(url);
            var contato = await clientAPI.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contato);
            ClassicAssert.IsTrue(contato.Email == $"{guid}@gmail.com");
            id = contato.Id;
        }

        [Test, Order(7)]
        public async Task Buscar_Id()
        {
            var url = $"Contato/Buscar/Id?id={id.ToString()}";

            var result = await clientAPI.GetAsync(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test, Order(5)]
        public async Task Atualizar_Contato()
        {
            var url = "/Contato/Atualizar";

            var contato = new Contato()
            {
                Id = id,
                Email = $"{guid}@gmail.com",
                Nome = $"{guid} - Atualizado",
                Telefone = new Telefone()
                {
                    DDD = "11",
                    Numero = "912345678"
                }
            };

            var result = await clientAPI.PutAsJsonAsync(url, contato);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            url = $"Contato/Buscar/Id?id={id.ToString()}";

            result = await clientAPI.GetAsync(url);
            contato = await clientAPI.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contato);
            ClassicAssert.IsTrue(contato.Nome == $"{guid} - Atualizado");
        }

        [Test, Order(6)]
        public async Task Excluir_Contato()
        {
            var url = $"/Contato/Excluir?id={id.ToString()}";

            var result = await clientAPI.DeleteAsync(url);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}