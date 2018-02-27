using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Model;
using backend.Model.Projection;

namespace backend.Service
{
    /// <summary>
    /// Service layer for doing stuff concerning the customer.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer.
        /// The needed values are supplied as parameters.
        /// </summary>
        /// <param name="customer">The given customer.</param>
        /// <param name="address">The given address.</param>
        /// <returns>The new customer.</returns>
        Task<Customer> NewCustomer(Customer customer, Address address);

        /// <summary>
        /// Updates a given customer.
        /// The needed values are supplied as parameters.
        /// </summary>
        /// <param name="customer">The given customer.</param>
        /// <param name="address">The given address.</param>
        /// <returns>The updated customer.</returns>
        Task<Customer> UpdateCustomer(Customer customer, Address address);

        /// <summary>
        /// Deletes a given customer.
        /// The needed values are supplied as parameters.
        /// </summary>
        /// <param name="customerId">The given customer´s id.</param>
        /// <returns>True or false.</returns>
        Task<bool> DeleteCustomer(Guid customerId);

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <returns>An ienumerable of all customer as a projction.</returns>
        Task<IEnumerable<CustomerProjection>> GetAllCustomer();

        /// <summary>
        /// Loads a customer with the corresponding address.
        /// </summary>
        /// <param name="id">The given customer id.</param>
        /// <returns>Customer</returns>
        Task<Customer> LoadOneCustomerIncludeAddress(Guid id);
    }
}
