using backend.Model.Enumeration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Model.Configuration
{
    /// <inheritdoc />
    /// <summary>
    /// Configuration settings for the entity Address.
    /// </summary>
    internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(address => address.Street).IsRequired();
            builder.Property(address => address.Street).HasMaxLength((int)MaxLength.Medium);

            builder.Property(address => address.PostalCode).IsRequired();
            builder.Property(address => address.PostalCode).HasMaxLength((int)MaxLength.Min);
            builder.HasIndex(address => address.PostalCode);

            builder.Property(address => address.City).IsRequired();
            builder.Property(address => address.City).HasMaxLength((int)MaxLength.Medium);
            builder.HasIndex(address => address.City);
        }
    }
}
