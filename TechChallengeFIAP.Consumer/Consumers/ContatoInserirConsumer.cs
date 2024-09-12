using MassTransit;
using System.Diagnostics;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Consumer.Consumers
{
    public class ContatoInserirConsumer : IConsumer<Contato>
    {
        private IContatoRepository _contatoRepository;

        public ContatoInserirConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task Consume(ConsumeContext<Contato> context)
        {
            try
            {
                Console.WriteLine(context.Message);

                await _contatoRepository.AddAsync(context.Message);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
