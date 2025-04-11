using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.ParametersTypes;

namespace Services
{

    //A primary constructor in C# is a new feature in C# 12 that allows defining constructor parameters
    //directly in the class or struct declaration
    class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {



        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {


            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(false);


            var Result = _mapper.Map<IEnumerable<BrandResultDto>>(brands);




            return Result;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductQueryParameters Parameters)
        {

            var Products = await _unitOfWork.GetRepository<Product, int>().
                GetAllWithSpecificationsAsync(new ProductWithTypeAndBrandSpecifications(Parameters));

            var TotalCount = await _unitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(Parameters));

            var Result = _mapper.Map<IEnumerable<ProductResultDto>>(Products);


            return new PaginatedResult<ProductResultDto>
            (

                Parameters.PageIndex,
                Result.Count(),
                TotalCount,
                Result

            );


        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {

            var ProductsTypes = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(false);


            var Result = _mapper.Map<IEnumerable<TypeResultDto>>(ProductsTypes);


            return Result;
        }

        public async Task<ProductResultDto> GetProductById(int id)
        {


            var Product = await _unitOfWork.GetRepository<Product, int>().
                GetWithIDSpecificationsAsync(new ProductWithTypeAndBrandSpecifications(id));


            var Result = _mapper.Map<ProductResultDto>(Product);


            return Result;
        }
    }
}
