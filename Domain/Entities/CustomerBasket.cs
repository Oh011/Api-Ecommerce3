namespace Domain.Entities
{
    public class CustomerBasket
    {

        public string id { get; set; }

        public IEnumerable<BasketItem> Items { get; set; }
    }

}
