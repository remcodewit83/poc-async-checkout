using System;
using System.Threading.Tasks;

namespace Application.Providers
{
    public interface IRetailerProvider
    {
        Task<string> ConfirmOrderAsync(Guid cartId);
    }
}
