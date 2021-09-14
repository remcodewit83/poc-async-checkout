using Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Providers
{
    public interface IPriceProvider
    {
        Task<CalculatePriceResponse> CalculatePriceAsync(CartProduct[] products, string country);
    }

    public class CalculatePriceResponse
    {
        public CartProduct[] Products { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
