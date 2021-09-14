using Application.Domain;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Store
{
    public class CartStore : ICartStore
    {
        private readonly MongoDbContext _context;
        public CartStore(MongoDbContext context)
        {
            _context = context;
            EnsureIndexes().Wait();
        }
        private async Task EnsureIndexes()
        {
            await _context.Carts.Indexes.CreateOneAsync(new CreateIndexModel<Cart>
            (Builders<Cart>.IndexKeys.Ascending(cart => cart.Id),
                new CreateIndexOptions() { }));
        }


        public Task CreateCartAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.InsertOneAsync(cart, new InsertOneOptions() {  }, cancellationToken);
        }

        public Task<Cart> GetCartByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _context.Carts.Find(c => c.Id == id).SingleOrDefaultAsync(cancellationToken);
        }

        public Task UpdateShopperDetailsAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            // Suck it Cosmos, with your lack of partial updates.
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.Shopper, cart.Shopper)
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);
        }

        public Task UpdateShippingDetailsAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.Shipping, cart.Shipping)
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);

        }

        public Task UpdateEddDatesAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.ExpectedEddStart, cart.ExpectedEddStart)
                    .Set(c => c.ExpectedEddEnd, cart.ExpectedEddEnd)
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);
        }

        public Task UpdatePricesAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.Products, cart.Products)
                    .Set(c => c.ShippingCost, cart.ShippingCost)
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);
        }
        public Task ConfirmCartAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.RetailerOrderReference, cart.RetailerOrderReference)
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);
        }
        public Task RequestToConfirmCartAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            return _context.Carts.UpdateOneAsync(
                c => c.Id == cart.Id,
                Builders<Cart>.Update
                    .Set(c => c.CurrentCartEvent, cart.CurrentCartEvent)
                    .Push(c => c.AllCartEvents, cart.CurrentCartEvent),
                cancellationToken: cancellationToken);
        }
    }
}
