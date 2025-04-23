namespace Shared.OrderModels
{
    public record OrderRequest
    {

        public string BasketId { get; set; }


        public AddressDto shippingAdress { get; set; }


        public int DeliveryMethodId { get; set; }
    }
}
