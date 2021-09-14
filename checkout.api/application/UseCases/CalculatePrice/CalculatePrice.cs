using System;

namespace Application.UseCases.CalculatePrice
{
    public class CalculatePrice
    {
        public CalculatePrice(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
