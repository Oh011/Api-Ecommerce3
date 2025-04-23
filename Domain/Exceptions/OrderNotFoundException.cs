namespace Domain.Exceptions
{
    public class OrderNotFoundException : Exception
    {

        public OrderNotFoundException(Guid id) : base($"Order with Id : {id} is not found") { }
    }
}
