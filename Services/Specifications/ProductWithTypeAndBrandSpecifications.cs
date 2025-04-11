using Domain.Contracts;
using Domain.Entities;
using Shared.ParametersTypes;

namespace Services.Specifications
{
    public class ProductWithTypeAndBrandSpecifications : Specifications<Product>
    {


        //Get Product By type --> brand , Type



        public ProductWithTypeAndBrandSpecifications(int id) : base(Product => Product.Id == id)
        {


            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);
        }

        public ProductWithTypeAndBrandSpecifications(ProductQueryParameters Parameters) : base(

            Product =>
            (!Parameters.BrandId.HasValue || Parameters.BrandId.Value == Product.BrandId) &&
            (!Parameters.TypeId.HasValue || Parameters.TypeId.Value == Product.TypeId) &&
            (string.IsNullOrEmpty(Parameters.Search) || Product.Name.ToLower().Contains(Parameters.Search.ToLower()))

            )
        {

            AddInclude(Product => Product.ProductBrand);
            AddInclude(Product => Product.ProductType);



            if (Parameters.Sort is not null)
            {

                switch (Parameters.Sort)
                {


                    case ProductSortOptions.PriceDesc:
                        SetOrderByDescending(Product => Product.Price);
                        break;


                    case ProductSortOptions.NameAsc:
                        SetOrderBy(Product => Product.Price);
                        break;

                    case ProductSortOptions.NameDesc:
                        SetOrderByDescending(Product => Product.Name);
                        break;


                    default:
                        SetOrderBy(Product => Product.Name);
                        break;



                }

            }



            ApplyPagination(Parameters.PageIndex, Parameters.PageSize);

        }
    }
}
