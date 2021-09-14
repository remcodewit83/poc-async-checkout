using Application.UseCases.CreateCart;
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
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateCartRequest>
        .WithResponse<CreateCartResponse>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public Create(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("")]
        [SwaggerOperation(
            Summary = "Creates a new cart",
            Description = "Creates a new cart",
            OperationId = "cart.create",
            Tags = new[] { "Cart" })
        ]
        public override async Task<ActionResult<CreateCartResponse>> HandleAsync(CreateCartRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = new CreateCartResponse();

            var cartId = Guid.NewGuid();
            try
            {
                var createCart = _mapper.Map<CreateCart>(request);
                createCart.Id = cartId;
                await _mediator.Send(createCart, cancellationToken);
            } catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            response.CartId = cartId;
            response.CartUrl = $"http://localhost:3000/checkout?cartId={cartId}";

            return Ok(response);
        }


    }
}
