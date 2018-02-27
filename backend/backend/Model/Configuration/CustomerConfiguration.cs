using backend.Model.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Model.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// Configuration settings for the entity Customer.
    /// </summary>
    internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasIndex(customer => customer.Number).IsUnique();
            builder.Property(customer => customer.Number).ValueGeneratedOnAdd();

            builder.Property(customer => customer.Salutation).IsRequired();
            builder.Property(customer => customer.Salutation).HasMaxLength((int)MaxLength.Min);

            builder.Property(customer => customer.FirstName).IsRequired();
            builder.Property(customer => customer.FirstName).HasMaxLength((int)MaxLength.Medium);
            builder.HasIndex(customer => customer.FirstName);

            builder.Property(customer => customer.LastName).IsRequired();
            builder.Property(customer => customer.LastName).HasMaxLength((int)MaxLength.Medium);
            builder.HasIndex(customer => customer.LastName);
            builder.Property(customer => customer.Memo).HasMaxLength((int)MaxLength.Large);
        }
    }
}
