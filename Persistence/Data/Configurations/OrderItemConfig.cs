using Domain.Entities.OrderEntities;

namespace Persistence.Data.Configurations
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.Property(O => O.Price).HasColumnType("decimal(18,3)");



            builder.OwnsOne(O => O.product, O => O.WithOwner());
            //owns
        }
    }
}


// Mapping fields or properties of a class to a different table (separate from the main table of the entity):


//builder.OwnsOne(O => O.product, ...)
//This tells EF Core that the product property of the entity is an owned type.
//Owned types are value objects that don’t have their own identity (primary key) and are stored in the same table as the owner entity.
//product here is a complex type (likely a class) defined inside the main entity.