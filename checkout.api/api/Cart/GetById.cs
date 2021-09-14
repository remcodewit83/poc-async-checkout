using Application.Exceptions;
using Application.UseCases.GetCartDetails;
using Ardalis.ApiEndpoints;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Cart
{
    [Route("api/cart")]
    public class GetById : BaseAsyncEndpoint
        .WithRequest<GetByIdCartRequest>
        .WithResponse<Application.Domain.Cart>
    {
        private readonly IMediator _mediator;
        public GetById(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [SwaggerOperation(
            Summary = "Get a cart by Id",
            Description = "Gets a cart by Id",
            OperationId = "cart.GetById",
            Tags = new[] { "Cart" })
        ]
        public override async Task<ActionResult<Application.Domain.Cart>> HandleAsync([FromRoute] GetByIdCartRequest request, CancellationToken cancellationToken)
        {
            var client = _mediator.CreateRequestClient<GetCartDetails>();
            var result = await client.GetResponse<Application.Domain.Cart>(new { request.Id });
            return Ok(result.Message);
        }
    }
}
