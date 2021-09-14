using System;

namespace Application.UseCases.ProvideShopperDetails
{
    public class ProvideShopperDetails
    {
        public Guid Id { get; set; }
        public ShopperDetails ShopperDetails { get; set;}

        public ProvideShopperDetails()
        {
            ShopperDetails = new ShopperDetails();
        }
    }

    public class ShopperDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }

}
