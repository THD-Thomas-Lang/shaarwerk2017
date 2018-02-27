using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using backend.Model.Enumeration;

namespace web.Models.Customer
{
    public sealed class CustomerDto
    {
        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Min, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Anrede")]
        public string Salutation { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Medium, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Medium, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Nachname")]
        public string LastName { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Display(Name = "Geburtstag")]
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [StringLength((int)MaxLength.Large, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Notizen")]
        public string Memo { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public AddressDto Address { get; set; }

        /// <summary>
        /// Customer factory method.
        /// Builts customers from customer dtos.
        /// </summary>
        /// <param name="customerDto">A given customer dto.</param>
        /// <returns>Customer</returns>
        public static backend.Model.Customer CustomerFactory(CustomerDto customerDto)
        {
            return new backend.Model.Customer(customerDto.Salutation, 
                customerDto.FirstName, customerDto.LastName, customerDto.BirthDay,
                customerDto.Memo);
        }

        /// <summary>
        /// Standard constructor.
        /// Builds the needed references.
        /// </summary>
        public CustomerDto()
        {
            BirthDay = DateTime.Now;
            CustomerId = Guid.NewGuid();
            Address = new AddressDto();
        }

        /// <inheritdoc />
        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed references.
        /// </summary>
        /// <param name="customer">A given customer.</param>
        public CustomerDto(backend.Model.Customer customer) : this()
        {
            if(customer.Addresses == null || !customer.Addresses.Any()) throw new ArgumentNullException(nameof(customer.Addresses));
            CustomerId = customer.CustomerId;
            Salutation = customer.Salutation;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            BirthDay = customer.BirthDay;
            Number = customer.Number;
            Memo = customer.Memo;

            Address = new AddressDto(customer.Addresses.FirstOrDefault());

        }
}
}
