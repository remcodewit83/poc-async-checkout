using Application.UseCases.ProvideShopperDetails;
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
    public class ShopperDetails : BaseAsyncEndpoint
        .WithRequest<ProvideShopperDetailsRequest>
        .WithResponse<ProvideShopperDetailsResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ShopperDetails(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("{id}/shopperdetails")]
        [SwaggerOperation(
            Summary = "Provide shopper details",
            Description = "Provide shopper details",
            OperationId = "cart.shopperdetails",
            Tags = new[] { "Cart" })
        ]
        public override async Task<ActionResult<ProvideShopperDetailsResponse>> HandleAsync([FromRoute] ProvideShopperDetailsRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ProvideShopperDetailsResponse();

            try
            {
                var provideShopperDetails = _mapper.Map<ProvideShopperDetails>(request);
                await _mediator.Send(provideShopperDetails, cancellationToken);
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            return Ok(response);
        }


    }
}
