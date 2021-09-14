using Application.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Providers
{
    public class PriceProvider : IPriceProvider
    {
        private readonly LatencyProvider _latencyProvider;
        public PriceProvider(LatencyProvider latencyProvider)
        {
            _latencyProvider = latencyProvider;
        }
        public async Task<CalculatePriceResponse> CalculatePriceAsync(CartProduct[] products, string country)
        {
            await _latencyProvider.RandomLatency();
            products[0].Tax = 21;
            return new CalculatePriceResponse()
            {
                ShippingCost = 15,
                Products = products
            };
        }
    }
}
