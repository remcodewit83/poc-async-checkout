using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Cart
{
    public class ProvideShopperDetailsRequest
    {
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
        
        [Required]
        [FromBody]
        public ShopperDetailsRequest ShopperDetails { get; set; }
    }

    public class ShopperDetailsRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
