using System.ComponentModel.DataAnnotations;

namespace Api.Cart
{
    public class CreateCartRequest
    {
        [Required]
        public CreateCartProductRequest[] Products { get; set; }
    }

    public class CreateCartProductRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please specify a quantity larger than {1}")]
        public int Quantity { get; set; } = 1;
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public decimal GrossPrice { get; set; }
    }
}
