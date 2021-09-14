using Application.Domain;
using System;

namespace Application.UseCases.CreateCart
{
    public class CreateCart
    {
        public Guid Id { get; set; }
        public CartProduct[] Products { get; set; }
    }
}
