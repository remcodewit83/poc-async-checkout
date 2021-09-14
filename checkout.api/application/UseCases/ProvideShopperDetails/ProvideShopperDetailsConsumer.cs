using Application.Domain.Events;
using Application.Exceptions;
using Application.Store;
using AutoMapper;
using MassTransit;
using MassTransit.KafkaIntegration;
using System.Threading.Tasks;

namespace Application.UseCases.ProvideShopperDetails
{
    public class ProvideShopperDetailsConsumer : IConsumer<ProvideShopperDetails>
    {
        private readonly ICartStore _store;
        private readonly ITopicProducer<CartUpdated> _producer;
        private readonly IMapper _mapper;
        public ProvideShopperDetailsConsumer(
            ICartStore store,
            ITopicProducer<CartUpdated> producer,
            IMapper mapper)
        {
            _store = store;
            _producer = producer;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<ProvideShopperDetails> context)
        {
            var cart = await _store.GetCartByIdAsync(context.Message.Id);
            if (cart == null)
                throw new NotFoundException($"Order with ID {context.Message.Id} was not found");
            cart.ProvideShopperDetails(
                context.Message.ShopperDetails.FirstName,
                context.Message.ShopperDetails.LastName,
                context.Message.ShopperDetails.AddressLine1,
                context.Message.ShopperDetails.AddressLine2,
                context.Message.ShopperDetails.PostalCode,
                context.Message.ShopperDetails.City,
                context.Message.ShopperDetails.State,
                context.Message.ShopperDetails.Country,
                context.Message.ShopperDetails.PhoneNumber,
                context.Message.ShopperDetails.EmailAddress
                );
            await _store.UpdateShopperDetailsAsync(cart);

            // Hackity-hack
            await _producer.Produce(_mapper.Map<CartUpdated>(cart));
        
        }
    }
}
