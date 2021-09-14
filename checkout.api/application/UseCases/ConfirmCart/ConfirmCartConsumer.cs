using Application.Domain.Events;
using Application.Exceptions;
using Application.Providers;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.ConfirmCart
{
    public class ConfirmCartConsumer : IConsumer<ConfirmCart>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        private readonly IRetailerProvider _retailerProvider;
        public ConfirmCartConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper,
            IRetailerProvider retailerProvider)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
            _retailerProvider = retailerProvider;
        }
        public async Task Consume(ConsumeContext<ConfirmCart> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");

            var orderNumber = await _retailerProvider.ConfirmOrderAsync(context.Message.Id);
            cart.OrderConfirmed(orderNumber);

            await _store.ConfirmCartAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
