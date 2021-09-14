using Application.Providers;
using Application.UseCases.RequestToConfirmCart;
using Ardalis.ApiEndpoints;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Latency
{
    [Route("api/latency")]
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateRequest>
        .WithResponse<UpdateResponse>
    {
        private readonly LatencyProvider _latencyProvider;
        public Update(LatencyProvider latencyProvider)
        {
            _latencyProvider = latencyProvider;
        }

        [HttpPost("")]
        [SwaggerOperation(
            Summary = "Confirm order",
            Description = "Confirm order",
            OperationId = "cart.confirm",
            Tags = new[] { "Cart" })

        ]
        public override async Task<ActionResult<UpdateResponse>> HandleAsync(UpdateRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _latencyProvider.SetLatency(
                request.FastestMs, 
                request.SlowestMs);
            
            return Ok(new UpdateResponse());
        }


    }
}
