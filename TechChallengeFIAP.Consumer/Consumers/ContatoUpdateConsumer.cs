using MassTransit;
using TechChallengeFIAP.Core.Entities;
using TechChallengeFIAP.Core.Interfaces;

namespace TechChallengeFIAP.Consumer.Consumers
{
    public class ContatoAtualizarConsumer : IConsumer<Contato>
    {
        private IContatoRepository _contatoRepository;

        public ContatoAtualizarConsumer(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task Consume(ConsumeContext<Contato> context)
        {
            Console.WriteLine(context.Message);

            await _contatoRepository.UpdateAsync(context.Message, context.Message);

            await Task.CompletedTask;
        }
    }
}
