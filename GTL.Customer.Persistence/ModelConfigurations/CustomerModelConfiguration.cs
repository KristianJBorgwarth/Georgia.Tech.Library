using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTL.Customer.Persistence.ModelConfigurations;

public class CustomerModelConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Customer> builder)
    {
        throw new NotImplementedException();
    }
}