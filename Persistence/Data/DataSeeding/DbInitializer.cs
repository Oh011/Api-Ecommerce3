using Domain.Contracts;
using System.Text.Json;

namespace Persistence.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {


        private readonly ApplicationDbContext _dbContext;

        public DbInitializer(ApplicationDbContext Context)
        {


            _dbContext = Context;
        }


        public async Task InitializeAsync()
        {
            try
            {


                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    await _dbContext.Database.MigrateAsync();

                }


                if (!_dbContext.ProductTypes.Any())
                {


                    var TypesData = await File.ReadAllTextAsync(@"..\Persistence\Data\DataSeeding\types.json");

                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);


                    if (Types is not null && Types.Any())
                    {


                        await _dbContext.AddRangeAsync(Types);
                        await _dbContext.SaveChangesAsync();
                    }
                }




                if (!_dbContext.ProductBrands.Any())
                {


                    var BrandsData = await File.ReadAllTextAsync(@"..\Persistence\Data\DataSeeding\brands.json");

                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);


                    if (Brands is not null && Brands.Any())
                    {


                        await _dbContext.AddRangeAsync(Brands);
                        await _dbContext.SaveChangesAsync();
                    }
                }




                if (!_dbContext.Products.Any())
                {


                    var ProductsData = await File.ReadAllTextAsync(@"..\Persistence\Data\DataSeeding\products.json");

                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);


                    if (Products is not null && Products.Any())
                    {


                        await _dbContext.AddRangeAsync(Products);
                        await _dbContext.SaveChangesAsync();
                    }
                }











            }


            catch
            {


            }



        }
    }
}
