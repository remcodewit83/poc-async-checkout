using Ardalis.GuardClauses;
using System;
using System.Linq;

namespace Application.Domain
{
    public class Cart
    {
        public CartEvent CurrentCartEvent { get; private set; } = CartEvent.Initial;
        public CartEvent[] AllCartEvents { get; private set; } = new CartEvent[] { };
        public Guid Id { get; private set; }
        public CartProduct[] Products { get; }
        public CartShopperDetails Shopper { get; private set; }
        public CartShippingDetails Shipping { get; private set; }
        public string RetailerOrderReference { get; private set; }
        public decimal Tax
        {
            get
            {
                return Products.Sum(p => p.TotalTax);
            }
        }
        public decimal GrossPrice
        {
            get
            {
                return Products.Sum(p => p.TotalGrossPrice);
            }
        }
        public decimal NetPrice
        {
            get
            {
                return Products.Sum(p => p.TotalNetPrice);
            }
        }

        public DateTimeOffset ExpectedEddStart { get; private set; }
        public DateTimeOffset ExpectedEddEnd { get; private set; }

        public decimal ShippingCost { get; private set; }

        public decimal TotalPrice
        {
            get
            {
                return ShippingCost + GrossPrice;
            }
        }

        public Cart(
            Guid id,
            CartProduct[] products)
        {
            Guard.Against.NullOrEmpty(id, nameof(id));
            Guard.Against.NullOrEmpty(products, nameof(products));

            Id = id;
            Products = products;
            CurrentCartEvent = CartEvent.Validated;
            AllCartEvents = new CartEvent[] { CurrentCartEvent };
        }

        public void PriceCalculated(
            CartProduct[] products,
            decimal shippingCost)
        {
            Guard.Against.Null(shippingCost, nameof(shippingCost));
            Guard.Against.NullOrEmpty(products, nameof(products));

            ShippingCost = shippingCost;
            foreach(var product in products)
            {
                Products.First(p => p.ProductCode == product.ProductCode).Tax = product.Tax;
            }

            CurrentCartEvent = CartEvent.PriceCalculated;
        }

        public void EddCalculated(DateTimeOffset start, DateTimeOffset end)
        {
            ExpectedEddStart = start;
            ExpectedEddEnd = end;

            CurrentCartEvent = CartEvent.ShippingEddCalculated;
        }

        public void RequestToConfirm()
        {
            CurrentCartEvent = CartEvent.RequestedToConfirm;
        }

        public void OrderConfirmed(string retailerOrderReference)
        {
            Guard.Against.NullOrWhiteSpace(retailerOrderReference, nameof(retailerOrderReference));

            RetailerOrderReference = retailerOrderReference;

            CurrentCartEvent = CartEvent.Confirmed;
        }

        public void ProvideShopperDetails(
            string firstName,
            string lastName,
            string addressLine1,
            string addressLine2,
            string postalCode,
            string city,
            string state,
            string country,
            string phoneNumber,
            string emailAddress)
        {
            Shopper = new CartShopperDetails(
                firstName,
                lastName,
                addressLine1,
                addressLine2,
                postalCode,
                city,
                state,
                country,
                phoneNumber,
                emailAddress);

            CurrentCartEvent = CartEvent.ShopperDetailsProvided;
        }

        public void ProvideShippingDetails(
            string addressLine1,
            string addressLine2,
            string postalCode,
            string city,
            string state,
            string country)
        {
            Shipping = new CartShippingDetails(
                addressLine1,
                addressLine2,
                postalCode,
                city,
                state,
                country);

            CurrentCartEvent = CartEvent.ShippingDetailsProvided;
        }
    }
}
