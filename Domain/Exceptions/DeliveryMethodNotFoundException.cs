namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException : Exception
    {


        public DeliveryMethodNotFoundException(string id) : base($"DeliveryMethod with Id : {id} not found")
        {

        }
    }
}
