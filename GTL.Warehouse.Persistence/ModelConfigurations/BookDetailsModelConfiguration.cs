using GTL.Warehouse.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.ModelConfigurations
{
    public class BookDetailsModelConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder.ToTable("BookDetails");
            builder.HasKey(bd => bd.Id);

            builder.Property(bd => bd.ISBN)
                .IsRequired()
                .HasMaxLength(13); // Example constraint for ISBN-13

            builder.Property(bd => bd.Publisher)
                .IsRequired()
                .HasMaxLength(200); // Example constraint

            builder.Property(bd => bd.PublishedDate)
                .IsRequired();
        }
    }
}
