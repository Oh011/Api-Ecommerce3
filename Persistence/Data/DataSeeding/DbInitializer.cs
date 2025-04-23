using Domain.Contracts;
using Domain.Entities.OrderEntities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistence.Data.DataSeeding
{
    public class DbInitializer : IDbInitializer
    {


        private readonly ApplicationDbContext _dbContext;

        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<User> _userManager;

        public DbInitializer(ApplicationDbContext Context, UserManager<User> userManager, RoleManager<IdentityRole> rolemanger)
        {


            _dbContext = Context;
            _userManager = userManager;
            _roleManager = rolemanger;
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

                if (!_dbContext.DeliveryMethods.Any())
                {


                    var DeliveryMethodsData = await File.ReadAllTextAsync(@"..\Persistence\Data\DataSeeding\delivery.json");

                    var Methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);


                    if (Methods is not null && Methods.Any())
                    {


                        await _dbContext.AddRangeAsync(Methods);
                        await _dbContext.SaveChangesAsync();
                    }
                }











            }


            catch
            {


            }



        }

        public async Task InitializeIdentityAsync()
        {
            //seed roles
            // seed users


            if (!_roleManager.Roles.Any())
            {


                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));






                if (!_userManager.Users.Any())
                {


                    var AdminUser = new User()
                    {

                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@Gmail.com",
                        PhoneNumber = "01145608910"


                    };


                    var superAdmin = new User()
                    {

                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@Gmail.com",
                        PhoneNumber = "01145608910"

                    };

                    await _userManager.CreateAsync(AdminUser, "Admin@12");
                    await _userManager.CreateAsync(superAdmin, "SuperAdmin@12");


                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                    await _userManager.AddToRoleAsync(superAdmin, "superAdmin");

                }
            }
        }
    }

}
