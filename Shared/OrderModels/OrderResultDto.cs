namespace Shared.OrderModels
{
    public record OrderResultDto
    {


        public Guid Id { get; set; }
        public string UserEmail { get; init; }


        public AddressDto ShippingAddress { get; init; } //owns
        //(composition 



        public ICollection<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();



        public string PaymentStatus { get; init; }


        public string DeliveryMethod { get; init; }



        public int? DeliveryMethodId { get; init; }


        public decimal Subtotal { get; init; }

        //for each order Item ==> OrderItem.price * quantity : subtotal
        // Total : Subtotal + ShippingPrice


        public DateTimeOffset OrderDate { get; init; }


        public string PaymentId { get; init; } = string.Empty;


        public decimal Total { get; init; }

    }
}
