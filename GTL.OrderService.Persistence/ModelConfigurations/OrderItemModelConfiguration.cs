using GTL.OrderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.OrderService.Persistence.ModelConfigurations
{
    public class OrderItemModelConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Tabelnavn
            builder.ToTable("OrderItem");

            // Primær nøgle
            builder.HasKey(o => o.Id);

            // Fremmednøgle: OrderId
            builder.Property(o => o.OrderId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.HasOne<Order>() // Relation til Order
                .WithMany() // Antag, at Order har mange OrderItems
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict); // Ingen cascading delete

            // Fremmednøgle: BookId (hvis den er reference til en Book-klasse)
            builder.Property(o => o.BookId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            // Bogtitel
            builder.Property(o => o.BookTitle)
                .IsRequired()
                .HasMaxLength(100) // Øger længdebegrænsning for titel
                .HasColumnType("nvarchar");

            // Pris
            builder.Property(o => o.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); // Standard decimal-type til penge

            // Antal
            builder.Property(o => o.Quantity)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}
