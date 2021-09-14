using System;

namespace Application.UseCases.ConfirmCart
{
    public class ConfirmCart
    {
        public ConfirmCart(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
