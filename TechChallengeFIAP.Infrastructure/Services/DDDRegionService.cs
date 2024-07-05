using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Infrastructure.Services
{
    public class DDDRegionService : IDDDRegionService
    {
        private readonly HttpClient client;
        public DDDRegionService(HttpClient httpClient)
        {
            client = httpClient;
        }

        /// <summary>
        /// Método que retorna informações da região sobre o DDD inserido
        /// </summary>
        /// <param name="pDDD"></param>
        /// <returns></returns>
        public async Task<DDDInfo?> GetInfo(string pDDD)
        {
            if (client.BaseAddress is null)
                client.BaseAddress = new Uri("https://brasilapi.com.br/api/ddd/v1/");

            var response = client.GetAsync($"{pDDD}").Result;

            DDDInfo getResponse = new();

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var getData = System.Text.Json.JsonSerializer.Deserialize<DDDInfo>(responseContent);
                getResponse = getData ?? getResponse;
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }

            await Task.CompletedTask;

            return getResponse;
        }
    }
}
