using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Services.Mapping
{
    public class ProductProfile : Profile
    {


        public ProductProfile()
        {


            CreateMap<ProductBrand, BrandResultDto>();

            CreateMap<ProductType, TypeResultDto>();



            CreateMap<Product, ProductResultDto>()
               .ForMember(dest => dest.BrandName, Options => Options.MapFrom(src => src.ProductBrand.Name))
               .ForMember(dest => dest.TypeName, Options => Options.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());


            //ForMember() is used when you need custom mapping for a specific property.
        }
    }
}
