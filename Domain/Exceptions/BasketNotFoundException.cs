namespace Domain.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {



        public BasketNotFoundException(string id) : base($" Basket with Id : {id} not found")
        {

        }
    }
}
