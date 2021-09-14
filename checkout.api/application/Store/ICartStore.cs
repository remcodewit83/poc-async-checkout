using Application.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Store
{
    public interface ICartStore
    {
        Task CreateCartAsync(Cart cart, CancellationToken cancellationToken = default);
        Task<Cart> GetCartByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateShopperDetailsAsync(Cart cart, CancellationToken cancellationToken = default);
        Task UpdateShippingDetailsAsync(Cart cart, CancellationToken cancellationToken = default);
        Task UpdateEddDatesAsync(Cart cart, CancellationToken cancellationToken = default);
        Task RequestToConfirmCartAsync(Cart cart, CancellationToken cancellationToken = default);
        Task ConfirmCartAsync(Cart cart, CancellationToken cancellationToken = default);
        Task UpdatePricesAsync(Cart cart, CancellationToken cancellationToken = default);
    }
}
