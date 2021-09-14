using Application.Domain;
using Application.Domain.Events;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.CreateCart
{
    public class CreateCartConsumer : IConsumer<CreateCart>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        public CreateCartConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<CreateCart> context)
        {
            var cart = new Cart(
                context.Message.Id,
                context.Message.Products
                );
            await _store.CreateCartAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
