using Domain.Contracts;
using Domain.Entities;
using Shared.ParametersTypes;


namespace Services.Specifications
{
    public class ProductCountSpecifications : Specifications<Product>
    {



        public ProductCountSpecifications(ProductQueryParameters Parameters) : base(

            Product =>
            (!Parameters.BrandId.HasValue || Parameters.BrandId.Value == Product.BrandId) &&
            (!Parameters.TypeId.HasValue || Parameters.TypeId.Value == Product.TypeId) &&
             (string.IsNullOrEmpty(Parameters.Search) || Product.Name.ToLower().Contains(Parameters.Search.ToLower()))

            )
        {



        }



    }

}
