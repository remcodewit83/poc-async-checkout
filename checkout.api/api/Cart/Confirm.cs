using Application.UseCases.RequestToConfirmCart;
using Ardalis.ApiEndpoints;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Cart
{
    [Route("api/cart")]
    public class Confirm : BaseAsyncEndpoint
        .WithRequest<ConfirmRequest>
        .WithResponse<ConfirmResponse>
    {
        private readonly IMediator _mediator;

        public Confirm(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/confirm")]
        [SwaggerOperation(
            Summary = "Confirm order",
            Description = "Confirm order",
            OperationId = "cart.confirm",
            Tags = new[] { "Cart" })
        ]
        public override async Task<ActionResult<ConfirmResponse>> HandleAsync([FromRoute] ConfirmRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new ConfirmResponse();

            try
            {
                await _mediator.Send(new RequestToConfirmCart() { Id = request.Id }, cancellationToken);
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            return Ok(response);
        }


    }
}
