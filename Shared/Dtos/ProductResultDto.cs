namespace Shared.Dtos
{
    public record ProductResultDto // Read-only data : Record
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }


        public decimal Price { get; set; }


        public string BrandName { get; set; }



        public string TypeName { get; set; }


    }
}


//A record is a reference type introduced in C# 9 that provides value-based equality rather than 
//reference-based equality (like class). It is mainly used for immutable data structures, such as DTOs,
//API responses, and read-only models.
