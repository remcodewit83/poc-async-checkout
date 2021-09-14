using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Cart
{
    public class ConfirmRequest
    {
        [Required]
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
