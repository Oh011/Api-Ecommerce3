﻿using Domain.Entities.OrderEntities;

namespace Persistence.Data.Configurations
{
    internal class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {


            builder.Property(d => d.Price).HasColumnType("decimal(18,3)");


        }
    }
}
