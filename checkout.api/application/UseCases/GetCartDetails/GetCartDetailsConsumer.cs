using Application.Exceptions;
using Application.Store;
using MassTransit;
using System.Threading.Tasks;

namespace Application.UseCases.GetCartDetails
{
    public class GetCartDetailsConsumer : IConsumer<GetCartDetails>
    {
        private readonly ICartStore _store;
        
        public GetCartDetailsConsumer(
            ICartStore store)
        {
            _store = store;
        }
        public async Task Consume(ConsumeContext<GetCartDetails> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);

            if(cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");

            await context.RespondAsync(cart);
        }
    }
}
