using Shared;
using Shared.Dtos;
using Shared.ParametersTypes;

namespace Services.Abstractions
{
    public interface IProductService
    {


        //Get all Products , brands , types , product by Id


        public Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductQueryParameters Parameters);



        public Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();



        public Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();




        public Task<ProductResultDto> GetProductById(int id);


    }
}
