using Application.Domain.Events;
using Application.Exceptions;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.ProvideShippingDetails
{
    public class ProvideShippingDetailsConsumer : IConsumer<ProvideShippingDetails>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        public ProvideShippingDetailsConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<ProvideShippingDetails> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");
            cart.ProvideShippingDetails(
                context.Message.ShippingDetails.AddressLine1,
                context.Message.ShippingDetails.AddressLine2,
                context.Message.ShippingDetails.PostalCode,
                context.Message.ShippingDetails.City,
                context.Message.ShippingDetails.State,
                context.Message.ShippingDetails.Country
                );
            await _store.UpdateShippingDetailsAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
