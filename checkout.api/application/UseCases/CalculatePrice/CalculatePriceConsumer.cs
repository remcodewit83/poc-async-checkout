using Application.Domain.Events;
using Application.Exceptions;
using Application.Providers;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.CalculatePrice
{
    public class CalculatePriceConsumer : IConsumer<CalculatePrice>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        private readonly IPriceProvider _priceProvider;
        public CalculatePriceConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper,
            IPriceProvider priceProvider)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
            _priceProvider = priceProvider;
        }
        public async Task Consume(ConsumeContext<CalculatePrice> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");

            var price = await _priceProvider.CalculatePriceAsync(cart.Products, cart.Shipping?.Country ?? cart.Shopper?.Country ?? "Ireland");
            cart.PriceCalculated(
                price.Products,
                price.ShippingCost
                );

            await _store.UpdatePricesAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
