using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Infrastracture.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastracture.Data.Configrations
{
    internal class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);
            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.OwnsOne(o => o.ShipToAddress, address => {
                address.Property(x => x.FirstName).HasMaxLength(50);
                address.Property(x => x.LastName).HasMaxLength(50);
                address.Property(x => x.City).HasMaxLength(50);
                address.Property(x => x.Street).HasMaxLength(50);
                address.Property(x => x.Country).HasMaxLength(50);
            });
            builder.Property(o => o.Status)
                   .HasConversion<string>().HasMaxLength(50);
        }
    }
}
