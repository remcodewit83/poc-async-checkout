using System;
using System.Threading.Tasks;

namespace Application.Providers
{
    public class EddProvider: IEddProvider
    {
        private readonly LatencyProvider _latencyProvider;
        public EddProvider(LatencyProvider latencyProvider)
        {
            _latencyProvider = latencyProvider;
        }
        public async Task<CalculateEddResponse> CalculateEddAsync(string country)
        {
            await _latencyProvider.RandomLatency();
            return new CalculateEddResponse()
            {
                EddStart = DateTimeOffset.Now.AddDays(5),
                EddEnd = DateTimeOffset.Now.AddDays(10)
            };
        }
    }
}
