using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Cart
{
    public class GetByIdCartRequest
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
