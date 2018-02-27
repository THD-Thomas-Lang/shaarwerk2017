using System;
using System.Threading.Tasks;
using backend.Model;
using backend.Model.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.DataSource
{
    /// <inheritdoc />
    /// <summary>
    /// Datasource context type.
    /// Used to exchange date from and to the datasource.
    /// The datasource is a postgresql database.
    /// </summary>
    public sealed class PostgreSqlDataContext : DbContext
    {
        private readonly ILogger _logger;

        public PostgreSqlDataContext(DbContextOptions<PostgreSqlDataContext> options,
            ILogger<PostgreSqlDataContext> logger) : base(options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("The Type {0} was sucessfully constructed at: {1}", GetType(), DateTime.Now);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());

            modelBuilder.Entity<Customer>().HasMany(customer => customer.Addresses).WithOne(address => address.Customer)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Address>().HasOne(address => address.Customer).WithMany(customer => customer.Addresses)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);

        }

        /// <summary>
        /// Commits all changes to the datasource.
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> CommitChanges()
        {
            try
            {
                await SaveChangesAsync();
                _logger.LogInformation(string.Format("Save changes called at: {0}", DateTime.Now));
                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError(string.Format("Save changes failed at: {0}", DateTime.Now), exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Given Customers.
        /// </summary>
        internal DbSet<Customer> Customers { get; set; }
    }
}
