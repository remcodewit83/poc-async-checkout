using System;
using System.Threading.Tasks;

namespace Application.Providers
{
    public class LatencyProvider
    {
        public int FastestMs { get; private set; } = 100;
        public int SlowestMs { get; private set; } = 2000;

        public void SetLatency(
            int fastestMs,
            int slowestMs)
        {
            FastestMs = fastestMs;
            SlowestMs = slowestMs;
        }

        public Task RandomLatency()
        {
            var random = new Random();
            return Task.Delay(random.Next(
                FastestMs, 
                SlowestMs));
        }
    }
}
