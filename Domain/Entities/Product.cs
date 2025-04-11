using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Product : BaseEntity<int>
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }


        public decimal Price { get; set; }


        public ProductBrand ProductBrand { get; set; }


        [ForeignKey(name: "ProductBrand")]
        public int BrandId { get; set; }


        public ProductType ProductType { get; set; }


        public int TypeId { get; set; }
    }
}
