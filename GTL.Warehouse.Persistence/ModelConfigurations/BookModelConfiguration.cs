using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities;

namespace GTL.Warehouse.Persistence.ModelConfigurations
{
    public class BookModelConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200); // Example constraint

            builder.Property(b => b.Price)
                .IsRequired()
                .HasMaxLength(50); // Example constraint, like a currency format

            builder.HasOne(b => b.BookDetails)
                .WithOne(bd => bd.Book)
                .HasForeignKey<BookDetails>(bd => bd.BookId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
