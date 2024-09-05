using MassTransit;
using TechChallengeFIAP.Core.Entities;

namespace TechChallenge.Consumer.Events
{
    public class ContatoUpdateConsumidor : IConsumer<Contato>
    {
        public async Task Consume(ConsumeContext<Contato> context)
        {
            Console.WriteLine(context.Message);

            await Task.CompletedTask;
        }
    }
}
