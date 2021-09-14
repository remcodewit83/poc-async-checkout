using Application.UseCases.ProvideShippingDetails;
using Ardalis.ApiEndpoints;
using AutoMapper;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Cart
{
    [Route("api/cart")]
    public class ShippingDetails : BaseAsyncEndpoint
        .WithRequest<ProvideShippingDetailsRequest>
        .WithResponse<ProvideShippingDetailsResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShippingDetails(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{id}/shippingdetails")]
        [SwaggerOperation(
            Summary = "Provide shipping details",
            Description = "Provide shipping details",
            OperationId = "cart.shippingdetails",
            Tags = new[] { "Cart" })
        ]
        public override async Task<ActionResult<ProvideShippingDetailsResponse>> HandleAsync([FromRoute] ProvideShippingDetailsRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ProvideShippingDetailsResponse();

            try
            {
                var provideShopperDetails = _mapper.Map<ProvideShippingDetails>(request);
                await _mediator.Send(provideShopperDetails, cancellationToken);
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            return Ok(response);
        }


    }
}
