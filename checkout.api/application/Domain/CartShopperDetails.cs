using Ardalis.GuardClauses;

namespace Application.Domain
{
    public class CartShopperDetails
    {
       public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string AddressLine1 { get; private set; }
        public string AddressLine2 { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string PhoneNumber { get; private set; }
        public string EmailAddress { get; private set; }

        public CartShopperDetails(
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
            Guard.Against.NullOrEmpty(firstName, nameof(firstName));
            Guard.Against.NullOrEmpty(lastName, nameof(lastName));
            Guard.Against.NullOrEmpty(addressLine1, nameof(addressLine1));
            Guard.Against.NullOrEmpty(postalCode, nameof(postalCode));
            Guard.Against.NullOrEmpty(city, nameof(city));
            Guard.Against.NullOrEmpty(state, nameof(state));
            Guard.Against.NullOrEmpty(country, nameof(country));
            Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));
            Guard.Against.NullOrEmpty(emailAddress, nameof(emailAddress));

            FirstName = firstName;
            LastName = lastName;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            PostalCode = postalCode;
            City = city;
            State = state;
            Country = country;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }
    }
}
