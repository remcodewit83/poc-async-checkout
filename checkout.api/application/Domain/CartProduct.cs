namespace Application.Domain
{
    public class CartProduct
    {
        public int Quantity { get; set; } = 1;
        public string ProductCode { get; set; }
        public decimal Tax { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal NetPrice
        {
            get
            {
                return GrossPrice - Tax;
            }
        }
        public decimal TotalTax
        {
            get
            {
                return Quantity * Tax;
            }
        }
        public decimal TotalGrossPrice
        {
            get
            {
                return Quantity * GrossPrice;
            }
        }
        public decimal TotalNetPrice
        {
            get
            {
                return Quantity * NetPrice;
            }
        }

    }
}
