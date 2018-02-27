using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DataSource;
using backend.Model;
using backend.Model.Projection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace backend.Service
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public sealed class CustomerService : ICustomerService
    {
        private readonly PostgreSqlDataContext _postgreSqlDataContext;
        private readonly ILogger<CustomerService> _logger;

        /// <summary>
        /// Overloaded constructor.
        /// Builts the needed dependencies.
        /// </summary>
        /// <param name="postgreSqlDataContext">A given datasource context.</param>
        /// <param name="logger">A given logger instance.</param>
        public CustomerService(PostgreSqlDataContext postgreSqlDataContext, 
            ILogger<CustomerService> logger)
        {
            _postgreSqlDataContext = postgreSqlDataContext ?? throw new ArgumentNullException(nameof(postgreSqlDataContext)); ;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="customer">A given (new) customer.</param>
        /// <param name="address">A given address.</param>
        /// <returns></returns>
        public async Task<Customer> NewCustomer(Customer customer, Address address)
        {
            try
            {
                customer.Addresses.Add(address);
                _postgreSqlDataContext.Customers.Add(customer);
                var result = await _postgreSqlDataContext.CommitChanges();
                return result ? customer : null;
            }
            catch (PostgresException postgresException)
            {
                _logger.LogError(postgresException.Message);
                return null;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <returns>An ienumerable of all customer as a projection.</returns>
        public async Task<IEnumerable<CustomerProjection>> GetAllCustomer()
        {
            return await _postgreSqlDataContext.Customers.AsQueryable().Select(s => new CustomerProjection()
            {
                CustomerId = s.CustomerId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Number = s.Number,
                // Joins all adresses by the ToString method to one big string property ...
                ShortAddress = string.Join(";", s.Addresses.Select(a => a.ToString()))

            }).AsNoTracking().ToListAsync();
        }

        /// <inheritdoc />
        /// <summary>
        /// Loads a customer with the corresponding address.
        /// </summary>
        /// <param name="id">The given customer id.</param>
        /// <returns>Customer</returns>
        public async Task<Customer> LoadOneCustomerIncludeAddress(Guid id) => await _postgreSqlDataContext.Customers.Include(c => c.Addresses).Where(s => s.CustomerId == id).FirstOrDefaultAsync();

        /// <inheritdoc />
        /// &lt;inheritdoc /&gt;
        /// <summary> 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomer(Customer customer, Address address)
        {
            try
            {
                customer.Addresses.Add(address);
                _postgreSqlDataContext.Entry(customer).State = EntityState.Modified;
                _postgreSqlDataContext.Update(customer);
                var result = await _postgreSqlDataContext.CommitChanges();
                return result ? customer : null;
            }
            catch (PostgresException postgresException)
            {
                _logger.LogError(postgresException.Message);
                return null;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes a given customer.
        /// The needed values are supplied as parameters.
        /// </summary>
        /// <param name="customerId">The given customer´s id.</param>
        /// <returns>True or false.</returns>
        public async Task<bool> DeleteCustomer(Guid customerId)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerId = customerId
                };
                _postgreSqlDataContext.Customers.Attach(customer);
                _postgreSqlDataContext.Customers.Remove(customer);
                await _postgreSqlDataContext.SaveChangesAsync();
                return true;
            }
            catch (PostgresException postgresException)
            {
                _logger.LogError(postgresException.Message);
                return false;
            }
        }
    }
}
