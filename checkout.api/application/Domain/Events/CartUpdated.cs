using System;
using System.Collections.Generic;

namespace Application.Domain.Events
{
    public class CartUpdated
    {
        public Guid Id { get; set; }
        public string RetailerOrderReference { get; set; }
        public CartShippingDetails Shipping { get; set; }
        public CartShopperDetails Shopper { get; set; }
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
        public CartEvent CurrentCartEvent { get; set; } = CartEvent.Initial;
    }
}
