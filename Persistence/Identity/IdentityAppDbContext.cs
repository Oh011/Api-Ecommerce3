using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence.Identity
{
    public class IdentityAppDbContext : IdentityDbContext<User>
    {




        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
        {

            //DbContextOptions --> Takes generic 

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Address>().ToTable("Address");
        }

    }
}


//have two DbContexts in your application (e.g., one for Identity and one for your app's domain):

//How to Add a Migration for a Specific Context:

//Add-Migration InitialIdentity -context IdentityAppDbContext -o Identity/Migrations

//To Update :
//Update-Database -context IdentityAppDbContext

//AspNetUsers table is the default table used by ASP.NET Identity to store user accounts

//Why the name AspNetUsers?
//It's just a convention used by ASP.NET Identity (default table names are AspNetUsers, AspNetRoles, etc.).