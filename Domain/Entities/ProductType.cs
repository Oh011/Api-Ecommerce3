namespace Domain.Entities
{
    public class ProductType : BaseEntity<int>
    {

        public string Name { get; set; }


        //Navigational property many to one {Product}
    }
}
