using Application.Domain.Events;
using Application.Exceptions;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.RequestToConfirmCart
{
    public class RequestToConfirmCartConsumer : IConsumer<RequestToConfirmCart>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        public RequestToConfirmCartConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<RequestToConfirmCart> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");
            cart.RequestToConfirm();
            await _store.RequestToConfirmCartAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
