namespace Shared.OrderModels
{
    public record OrderItemDto
    {

        public int ProductId { get; init; }


        public string ProductName { get; init; } //--> In Order : ProductInOrderItem product , product.ProductName


        public string PictureUrl { get; init; }

        public int Quantity { get; init; }


        public decimal Price { get; init; }
    }
}
