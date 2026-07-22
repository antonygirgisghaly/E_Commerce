using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Data.Configrations
{
    internal class DelevirayMethodConfigration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(b => b.Cost).HasColumnType("decimal(18,2)");

            builder.Property(b => b.ShortName).HasMaxLength(50);

            builder.Property(b => b.Description).HasMaxLength(100);

            builder.Property(b => b.DeliveryTime).HasMaxLength(50);
        }
    }
}
