using NUnit.Framework.Legacy;
using System.Net;
using System.Net.Http.Json;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.IntegrationTests
{
    [TestFixture]
    public class IntegrationTests : IDisposable
    {
        private IntegrationTestTechChallengeFIAPAPI FIAPAPI;
        private HttpClient clientAPI;
        private string guid = Guid.NewGuid().ToString();
        private IntegrationTestTechChallengeFIAPConsumer FIAPConsumer;
        private HttpClient clientConsumer;
        private int Id;

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

        public void Dispose()
        {
            FIAPAPI?.Dispose();
            clientAPI?.Dispose();
            FIAPConsumer?.Dispose();
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

            clientConsumer = FIAPConsumer.CreateClient();
            await Task.Delay(3000);
            Id = await BuscarId();
        }

        //[Test, Order(2)]
        public async Task Buscar_DDD()
        {
            var url = "/Contato/Buscar/DDD?DDD=11";

            var result = await clientAPI.GetAsync(url);
            var contatos = await clientAPI.GetFromJsonAsync<IEnumerable<Contato>>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contatos);
            var contato = contatos.Where(x => x.Nome == guid).Single();
            ClassicAssert.IsTrue(contato.Nome.Equals(guid));
        }

        //[Test, Order(3)]
        public async Task Buscar_Nome()
        {
            var url = $"/Contato/Buscar/Nome?nome={guid}";

            var result = await clientAPI.GetAsync(url);
            var contato = await clientAPI.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contato);
            ClassicAssert.IsTrue(contato.Email == $"{guid}@gmail.com");
        }

        //[Test, Order(6)]
        public async Task Buscar_Id()
        {
            var url = $"Contato/Buscar/Id?id={Id.ToString()}";

            var result = await clientAPI.GetAsync(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        //[Test, Order(4)]
        public async Task Atualizar_Contato()
        {
            var url = "/Contato/Atualizar";

            var contatoAtualizado = new Contato()
            {
                Id = Id,
                Email = $"{guid}@gmail.com",
                Nome = $"{guid} - Atualizado",
                Telefone = new Telefone()
                {
                    DDD = "11",
                    Numero = "912345678"
                }
            };

            var result = await clientAPI.PutAsJsonAsync(url, contatoAtualizado);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            clientConsumer = FIAPConsumer.CreateClient();
            await Task.Delay(3000);

            url = $"Contato/Buscar/Id?id={Id.ToString()}";

            result = await clientAPI.GetAsync(url);
            var contato = await clientAPI.GetFromJsonAsync<Contato>(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            ClassicAssert.IsNotNull(contato);
            ClassicAssert.IsTrue(contato.Nome == $"{guid} - Atualizado");
        }

        private async Task<int> BuscarId()
        {
            var url = $"/Contato/Buscar/Nome?nome={guid}";
            var result = await clientAPI.GetAsync(url);
            var contato = await clientAPI.GetFromJsonAsync<Contato>(url);
            var r = contato is null ? 0 : contato.Id;
            return r;
        }

        //[Test, Order(5)]
        public async Task Excluir_Contato()
        {
            var url = $"/Contato/Excluir?id={Id.ToString()}";

            var result = await clientAPI.DeleteAsync(url);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            clientConsumer = FIAPConsumer.CreateClient();
            await Task.Delay(3000);
        }
    }
}
