using MassTransit;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Consumer.Consumers
{
    public class ContatoExcluirConsumer : IConsumer<Contato>
    {
        private IContatoRepository _contatoRepository;

        public ContatoExcluirConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task Consume(ConsumeContext<Contato> context)
        {
            Console.WriteLine(context.Message);

            await _contatoRepository.DeleteAsync(context.Message);

            await Task.CompletedTask;
        }
    }
}
