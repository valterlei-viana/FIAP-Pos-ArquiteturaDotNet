
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework.Legacy;
using System.Net;
using System.Net.Http.Json;
using TechChallengeFIAP.Core.Entities;

namespace TechChallengeFIAP.IntegrationTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private IntegrationTestTechChallengeFIAPAPI _FIAPAPI;
        private IntegrationTestTechChallengeFIAPConsumer _FIAPConsumer;
        private HttpClient _clientAPI, _clientConsumer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _FIAPAPI = new IntegrationTestTechChallengeFIAPAPI();
            //_FIAPConsumer = new IntegrationTestTechChallengeFIAPConsumer();
            //_clientConsumer = _FIAPConsumer.CreateClient();
            _clientAPI = _FIAPAPI.CreateClient();
        }

        [SetUp]
        public void Setup()
        {
        }

        public void Dispose()
        {
            _FIAPConsumer?.Dispose();
            _FIAPAPI?.Dispose();
            _clientAPI?.Dispose();
            _clientConsumer?.Dispose();
        }

        //[Test, Order(1)]
        //public async Task Inserir_Contato()
        //{
        //    var url = "/Contato/Inserir";

        //    var contato = new Contato()
        //    {
        //        Email = "valterlei.viana@gmail.com",
        //        Nome = "Valterlei",
        //        Telefone = new Telefone()
        //        {
        //            DDD = "99",
        //            Numero = "994870098"
        //        }
        //    };

        //    var result = await _clientAPI.PostAsJsonAsync(url, contato);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        //}


        //[Test, Order(2)]
        //public async Task Buscar_DDD()
        //{
        //    var url = "/Contato/Buscar/DDD?DDD=99";

        //    var result = await _clientAPI.GetAsync(url);
        //    var contato = await _clientAPI.GetFromJsonAsync<IEnumerable<Contato>>(url);

        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    ClassicAssert.IsNotNull(contato);
        //    ClassicAssert.IsTrue(contato.First().Email == "valterlei.viana@gmail.com");
        //}

        //[Test, Order(3)]
        //public async Task Buscar_Nome()
        //{
        //    var url = $"/Contato/Buscar/Nome?nome=Valterlei";

        //    var result = await _clientAPI.GetAsync(url);
        //    var contato = await _clientAPI.GetFromJsonAsync<Contato>(url);

        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    ClassicAssert.IsNotNull(contato);
        //    ClassicAssert.IsTrue(contato.Email == "valterlei.viana@gmail.com");
        //}

        [Test, Order(7)]
        public async Task Buscar_Id()
        {
            var url = "Contato/Buscar/Id?id=1";

            var result = await _clientAPI.GetAsync(url);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        //[Test, Order(5)]
        //public async Task Atualizar_Contato()
        //{
        //    var url = "/Contato/Atualizar";

        //    var contato = new Contato()
        //    {
        //        Email = "valterlei.test@gmail.com",
        //        Nome = "Valterlei - Atualizado",
        //        Telefone = new Telefone()
        //        {
        //            DDD = "99",
        //            Numero = "994870098"
        //        }
        //    };

        //    var result = await _clientAPI.PutAsJsonAsync(url, contato);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        //    url = $"/Contato/Buscar/Nome?nome=" + "Valterlei - Atualizado";

        //    result = await _clientAPI.GetAsync(url);
        //    contato = await _clientAPI.GetFromJsonAsync<Contato>(url);

        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    ClassicAssert.IsNotNull(contato);
        //    ClassicAssert.IsTrue(contato.Email == "valterlei.viana@gmail.com");
        //}

        //[Test, Order(6)]
        //public async Task Excluir_Contato()
        //{
        //    var url = "/Contato/Excluir?id=1";

        //    var result = await _clientAPI.DeleteAsync(url);
        //    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //}
    }
}