using Ardalis.GuardClauses;

namespace Application.Domain
{
    public class CartShippingDetails
    {
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        public CartShippingDetails(
            string addressLine1,
            string addressLine2,
            string postalCode,
            string city,
            string state,
            string country)
        {
            Guard.Against.NullOrEmpty(addressLine1, nameof(addressLine1));
            Guard.Against.NullOrEmpty(postalCode, nameof(postalCode));
            Guard.Against.NullOrEmpty(city, nameof(city));
            Guard.Against.NullOrEmpty(state, nameof(state));
            Guard.Against.NullOrEmpty(country, nameof(country));

            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            State = state;
            Country = country;
        }
    }
}
