using Application.Domain.Events;
using Application.Hubs;
using Application.UseCases.CalculateEdd;
using Application.UseCases.CalculatePrice;
using Application.UseCases.ConfirmCart;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Application.Consumers
{
    public class KafkaConsumer : IConsumer<CartUpdated>
    {
        private readonly IHubContext<StatusHub> _statusHub;
        private readonly IMediator _mediator;
        public KafkaConsumer(
            IHubContext<StatusHub> statusHub,
            IMediator mediator)
        {
            _statusHub = statusHub;
            _mediator = mediator;
        }
        public Task Consume(ConsumeContext<CartUpdated> context)
        {
            var tasks = new Task[0];
            switch(context.Message.CurrentCartEvent)
            {
                case Domain.CartEvent.Validated:
                case Domain.CartEvent.ShippingDetailsProvided:
                case Domain.CartEvent.ShopperDetailsProvided:
                    tasks = new[] {
                        _mediator.Send(new CalculatePrice(context.Message.Id)),
                        _mediator.Send(new CalculateEdd(context.Message.Id))
                    };
                    break;
                case Domain.CartEvent.ShippingEddCalculated:
                case Domain.CartEvent.PriceCalculated:
                    tasks = new[] { 
                        _statusHub.Clients.Groups(context.Message.Id.ToString())
                            .SendAsync("StateChanged", context.Message.CurrentCartEvent.ToString()) };
                    break;
                case Domain.CartEvent.Confirmed:
                    tasks = new[] {
                        _statusHub.Clients.Groups(context.Message.Id.ToString())
                            .SendAsync("StateChanged", context.Message.CurrentCartEvent.ToString(), context.Message.RetailerOrderReference)
                    };
                    break;
                case Domain.CartEvent.RequestedToConfirm:
                    tasks = new[] { _mediator.Send(new ConfirmCart(context.Message.Id)) };
                    break;
            }
            Task.WaitAll(tasks);
            return Task.CompletedTask;
        }
    }
}
