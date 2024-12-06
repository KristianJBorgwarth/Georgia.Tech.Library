using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTL.Customer.Persistence.ModelConfigurations;

public class CustomerModelConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(x => x.Address).HasColumnName("Email").IsRequired();
        });
        builder.OwnsOne(x => x.Password, password =>
        {
            password.Property(x => x.HashedPassword).HasColumnName("Password").IsRequired();
        });
    }
}