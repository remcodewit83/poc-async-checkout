using System;
using System.Threading.Tasks;

namespace Application.Providers
{
    public interface IEddProvider
    {
        Task<CalculateEddResponse> CalculateEddAsync(string country);
    }

    public class CalculateEddResponse
    {
        public DateTimeOffset EddStart { get; set; }
        public DateTimeOffset EddEnd { get; set; }
    }
}
