using GTL.Warehouse.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.ModelConfigurations
{
    public class SellerModelConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("Seller");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100); // Example constraint

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(200); // Example constraint

            builder.HasMany(s => s.Books)
                .WithOne(b => b.Seller)
                .HasForeignKey(b => b.SellerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
