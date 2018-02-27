using System;
using System.Threading.Tasks;
using backend.Model;
using backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web.Models.Customer;

namespace web.Controllers.Customer
{
    /// <inheritdoc />
    /// <summary>
    /// Customer Controller.
    /// Handles the customer /customer routes.
    /// </summary>
    public sealed class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed dependencies.
        /// </summary>
        /// <param name="logger">A given logger reference.</param>
        /// <param name="customerService">A given customer service layer reference.</param>
        public CustomerController(ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handles /customer/index or /customer/ routes.
        /// Displays all customer.
        /// </summary>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Index()
        {
            var result = await _customerService.GetAllCustomer();
            return View(result);
        }

        /// <summary>
        /// Handles /customer/add routes.
        /// Adds a new customer.
        /// </summary>
        /// <returns>IActionResult</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Add(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(customerDto);
            }
            var customer = new backend.Model.Customer(customerDto.Salutation, customerDto.FirstName,
                customerDto.LastName, customerDto.BirthDay,
                customerDto.Memo);
            var address = new Address(customerDto.Address.Street, customerDto.Address.PostalCode, customerDto.Address.City,
                customerDto.Address.Country, customerDto.Address.PhoneNumbers, customerDto.Address.Emails,
                customerDto.Address.Socials);

            await _customerService.NewCustomer(customer, address);
            _logger.LogInformation(string.Format("Customer {0} with Address {0} created ...", customer.ToString(), address.ToString()));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles /customer/edit routes.
        /// Adds a new customer.
        /// </summary>
        /// <returns>IActionResult</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", customerDto);
            }
            var customer = new backend.Model.Customer(customerDto.CustomerId, 
                customerDto.Number, customerDto.Salutation, customerDto.FirstName,
                customerDto.LastName, customerDto.BirthDay,
                customerDto.Memo);
            var address = new Address(customerDto.Address.AddressId, customerDto.Address.Street, customerDto.Address.PostalCode, customerDto.Address.City,
                customerDto.Address.Country, customerDto.Address.PhoneNumbers, customerDto.Address.Emails,
                customerDto.Address.Socials);

            await _customerService.UpdateCustomer(customer, address);
            _logger.LogInformation(string.Format("Customer {0} with Address {0} updated ...", customer.ToString(), address.ToString()));
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Handles /customer/add routes.
        /// Adds an customer.
        /// </summary>
        /// <returns>IActionResult</returns>
        public IActionResult Add()
        {
            return View(new CustomerDto());
        }

        /// <summary>
        /// Edits the given customer.
        /// </summary>
        /// <param name="id">The customer´s id.</param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await _customerService.LoadOneCustomerIncludeAddress(id);
            if(customer == null) throw new ArgumentNullException(nameof(customer));
            return View("Edit", new CustomerDto(customer));
        }

        /// <summary>
        /// Deletes the given customer.
        /// </summary>
        /// <param name="id">The customer´s id.</param>
        /// <returns>IActionResult</returns>
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _customerService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}