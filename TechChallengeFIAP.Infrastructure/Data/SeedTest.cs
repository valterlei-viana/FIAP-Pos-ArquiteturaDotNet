using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Infrastructure.Data
{
    public static class SeedTest
    {
        public static void Add(FiapDbContext pFiapContext, IDDDRegionService pDddService)
        {
            var testeContato1 = new Contato
            {
                Id = 123,
                Nome = "Teste1",
                Email = "teste1@gmail.com",
                Telefone = new Telefone { DDD = "11", Numero = "982598878" }
            };

            var dddInfo = pDddService.GetInfo("11").Result;
            testeContato1.Telefone.UF = dddInfo?.UF;
            pFiapContext.Add(testeContato1);

            var testeContato2 = new Contato
            {
                Id = 456,
                Nome = "Teste2",
                Email = "teste2@gmail.com",
                Telefone = new Telefone { DDD = "21", Numero = "982599979", UF = "RJ" }
            };

            dddInfo = pDddService.GetInfo("21").Result;
            testeContato2.Telefone.UF = dddInfo?.UF;
            pFiapContext.Add(testeContato2);
            pFiapContext.SaveChanges();
        }
    }
}
