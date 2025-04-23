using Domain.Contracts;
using Domain.Entities.OrderEntities;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications : Specifications<Order>
    {
        public OrderWithIncludeSpecifications(Guid id) : base(o => o.Id == id)
        {

            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }


        //get all orders for a user

        public OrderWithIncludeSpecifications(string email) : base(o => o.UserEmail == email)
        {

            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            SetOrderBy(o => o.OrderDate);
        }
    }
}
