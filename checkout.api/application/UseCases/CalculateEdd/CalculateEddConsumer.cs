using Application.Domain.Events;
using Application.Exceptions;
using Application.Providers;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.CalculateEdd
{
    public class CalculateEddConsumer : IConsumer<CalculateEdd>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        private readonly IEddProvider _eddProvider;
        public CalculateEddConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper,
            IEddProvider eddProvider)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
            _eddProvider = eddProvider;
        }
        public async Task Consume(ConsumeContext<CalculateEdd> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");

            var edd = await _eddProvider.CalculateEddAsync(cart.Shipping?.Country ?? cart.Shopper?.Country ?? "Ireland");
            cart.EddCalculated(
                edd.EddStart,
                edd.EddStart
                );

            await _store.UpdateEddDatesAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
