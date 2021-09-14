using System;

namespace Application.UseCases.CalculateEdd
{
    public class CalculateEdd
    {
        public CalculateEdd(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
