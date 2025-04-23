using Domain.Entities.OrderEntities;

using OrderEntity = Domain.Entities.OrderEntities.Order;

namespace Persistence.Data.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {


            builder.OwnsOne(O => O.ShippingAddress, O => O.WithOwner());


            builder.HasMany(O => O.OrderItems).WithOne();


            builder.Property(O => O.PaymentStatus).HasConversion(PaymentStatus => PaymentStatus.ToString(),

                s => Enum.Parse<OrderPaymentStatus>(s)
                );


            builder.HasOne(O => O.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);



            builder.Property(O => O.Subtotal).HasColumnType("decimal(18,3)");


        }
    }
}
