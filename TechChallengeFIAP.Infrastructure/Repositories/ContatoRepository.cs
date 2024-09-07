using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;
using TechChallengeFIAP.Infrastructure.Data;

namespace TechChallengeFIAP.Infrastructure.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private FiapDbContext FiapContext;
        private IDDDRegionService DDDService;

        public ContatoRepository(FiapDbContext pFiapContext, IDDDRegionService pDDDService)
        {
            DDDService = pDDDService;
            FiapContext = pFiapContext;
        }

        /// <summary>
        /// Adiciona um contato
        /// </summary>
        /// <param name="pContato"></param>
        /// <returns></returns>
        public async Task<Contato> AddAsync(Contato pContato)
        {
            bool emailregistrado = await CheckRegisteredEmail(pContato);

            if (emailregistrado)
            {
                var dddInfo = await DDDService.GetInfo(pContato.Telefone.DDD);
                pContato.Telefone.UF = dddInfo?.UF;
                FiapContext.Add(pContato);
            }
            await FiapContext.SaveChangesAsync();
            return pContato;
        }

        /// <summary>
        /// Deleta um contato recebendo o id como parâmetro para encontrar o contato
        /// </summary>
        /// <param name="pContato"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Contato pContato)
        {
            var contatoDelete = await FindAsync(pContato.Id);

            if (contatoDelete != null)
            {
                FiapContext.Contatos.Remove(contatoDelete);
                await FiapContext.SaveChangesAsync();
            }
        }


        /// <summary>
        /// Busca e retorna um contato pelo seu id no banco, recebendo um id
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Contato?> FindAsync(int pID)
        {
            var contato = await FiapContext.Contatos.Include(x => x.Telefone).FirstOrDefaultAsync(x => x.Id == pID);
            return contato;
        }

        /// <summary>
        /// Retorna um contato pelo nome
        /// </summary>
        /// <param nome="pNome"></param>
        /// <returns></returns>
        /// <exception cref="WarningException"></exception>
        public async Task<Contato> GetByNameAsync(string pNome)
        {
            var contato = await FiapContext.Contatos
                                     .Include(x => x.Telefone)
                                     .Where(x => x.Nome == pNome)
                                     .FirstOrDefaultAsync();

            if (contato != null)
                return contato;
            else
                throw new WarningException($"Contato com este nome não encontrado");
        }

        /// <summary>
        /// Checa se o e-mail inserido já foi cadastrado no sistema recebendo o objeto contato como parâmetro
        /// </summary>
        /// <param name="pContato"></param>
        /// <returns></returns>
        /// <exception cref="WarningException"></exception>
        public async Task<bool> CheckRegisteredEmail(Contato pContato)
        {
            int contatos = CountAsync().Result;

            if (contatos > 0)
            {
                var emailChecker = FiapContext.Contatos.Where(c => c.Email == pContato.Email);

                if (emailChecker.Any())
                    throw new WarningException($"O email inserido já está cadastrado");
            }

            await Task.CompletedTask;

            return true;
        }

        /// <summary>
        /// Faz uma busca os contatos de acordo com o DDD fornecido ou retorna todos os contatos se não informado
        /// </summary>
        /// <param name="pDDD"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Contato>> GetAllAsync(string? pDDD)
        {
            var contatos = await FiapContext.Contatos
                .Include(x => x.Telefone)
                .Where(x => pDDD == x.Telefone.DDD || pDDD == null)
                .ToListAsync();

            return contatos;
        }

        /// <summary>
        /// Atualiza o contato
        /// </summary>
        /// <param name="pContatoAtual">Contato atual</param>
        /// <param name="pContatoAtualizado">Contato com dados atualizados</param>
        /// <returns></returns>
        public async Task UpdateAsync(Contato pContatoAtual, Contato pContatoAtualizado)
        {
            pContatoAtual.Nome = pContatoAtualizado.Nome;
            pContatoAtual.Email = pContatoAtualizado.Email;
            pContatoAtual.Telefone.DDD = pContatoAtualizado.Telefone.DDD;
            pContatoAtual.Telefone.Numero = pContatoAtualizado.Telefone.Numero;

            FiapContext.Update(pContatoAtual);
            await FiapContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retorna a quantidade de registros inseridos na base
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await FiapContext.Contatos.CountAsync();
        }
    }
}
