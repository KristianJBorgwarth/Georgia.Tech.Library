using GTL.OrderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTL.OrderService.Persistence.ModelConfigurations;

public class OrderModelConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        // Primær nøgle
        builder.HasKey(o => o.Id);

        // Fremmednøgle: UserId
        builder.Property(o => o.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        // Fremmednøgle: OrderItemId
        builder.Property(o => o.OrderItemId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.HasOne<OrderItem>() // Antag, at der er en OrderItem-klasse
            .WithMany() // Hvis OrderItem har mange Orders
            .HasForeignKey(o => o.OrderItemId)
            .OnDelete(DeleteBehavior.Restrict); // Juster sletningsadfærd

        builder.Property(o => o.OrderStatus)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("nvarchar");
    }
}