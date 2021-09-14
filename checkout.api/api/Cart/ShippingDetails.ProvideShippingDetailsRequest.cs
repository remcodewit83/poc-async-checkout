using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Cart
{
    public class ProvideShippingDetailsRequest
    {
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }

        [Required]
        [FromBody]
        public ShippingDetailsRequest ShippingDetails { get; set; }

    }

    public class ShippingDetailsRequest
    {
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
    }
}
