namespace Application.Domain
{
    public enum CartEvent
    {
        Initial = 0,
        Validated = 1,
        ShopperDetailsProvided= 2,
        ShippingDetailsProvided = 3,
        PriceCalculated = 4,
        ShippingEddCalculated = 5,
        RequestedToConfirm = 6,
        Confirmed = 7
    }
}
