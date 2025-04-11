using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;

namespace Services.Mapping
{
    public class PictureUrlResolver(IConfiguration _configurations) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {



            if (string.IsNullOrWhiteSpace(source.PictureUrl))
            {

                return string.Empty;
            }


            return $"{_configurations["BaseUrl"]}/{source.PictureUrl}";
        }
    }



}


//resolver is a function or delegate that you can use to customize how a property is mapped 
//between two objects. It provides a way to implement complex mapping logic that can't be 
//achieved through simple property-to-property mapping.


//use a resolver when:

//1. You need to combine multiple source properties to compute a destination property.

//2. You need to map a property using custom logic (like formatting or calculation).

//3. You want to conditionally map properties based on some business rule or logic.

//4. You need to map from a source object to a destination object with a more complex mapping than simple property
//matching.