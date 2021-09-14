using System;

namespace Api.Cart
{
    public class CreateCartResponse 
    {
        public Guid CartId { get; set; }
        public string CartUrl { get; set; }
    }
}
