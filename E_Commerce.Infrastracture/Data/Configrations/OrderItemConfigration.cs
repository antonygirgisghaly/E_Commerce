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
    internal class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(x => x.Product,
                x =>
                {
                    x.Property(p => p.ProductName)
                        .HasMaxLength(100);
                    x.Property(p => p.PictureUrl)
                        .HasMaxLength(200);
                });
        }
    }
}
