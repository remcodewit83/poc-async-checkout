using System;

namespace Application.UseCases.ProvideShippingDetails
{
    public class ProvideShippingDetails
    {
        public Guid Id { get; set; }

        public ShippingDetails ShippingDetails { get; set; }

        public ProvideShippingDetails()
        {
            ShippingDetails = new ShippingDetails();
        }

    }

    public class ShippingDetails
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
