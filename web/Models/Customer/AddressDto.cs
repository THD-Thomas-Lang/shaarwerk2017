using System;
using System.ComponentModel.DataAnnotations;
using backend.Model;
using backend.Model.Enumeration;

namespace web.Models.Customer
{
    public sealed class AddressDto
    {
        /// <summary>
        /// Standard constructor.
        /// </summary>
        public AddressDto()
        {
            AddressId = Guid.NewGuid();
        }

        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed references.
        /// </summary>
        /// <param name="address">A given address</param>
        public AddressDto(Address address)
        {
            if(address == null) throw new ArgumentNullException(nameof(address));
            AddressId = address.AddressId;
            City = address.City;
            PostalCode = address.PostalCode;
            Country = address.Country;
            PhoneNumbers = address.PhoneNumbers;
            Socials = address.Socials;
            Emails = address.Emails;
            Street = address.Street;
        }

        /// <summary>
        /// The given property.
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Medium, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Straße")]
        public string Street { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Min, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Postleitzahl")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Required(ErrorMessage = "Das Feld darf nicht null/leer sein!")]
        [StringLength((int)MaxLength.Medium, ErrorMessage = "Das Feld darf {1} Zeichen nicht überschreiten!")]
        [Display(Name = "Stadt")]
        public string City { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Display(Name = "Land")]
        public string Country { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Display(Name = "Telefonnummern")]
        public string PhoneNumbers { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Display(Name = "E-Mails")]
        public string Emails { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        [Display(Name = "Socials")]
        public string Socials { get; set; }

        /// <summary>
        /// Address factory method.
        /// Builts addresses from address dtos.
        /// </summary>
        /// <param name="addressDto">A given address dto.</param>
        /// <returns>Customer</returns>
        public static Address AddressFactory(AddressDto addressDto)
        {
            return new Address(addressDto.Street, addressDto.PostalCode, addressDto.City,
                addressDto.Country, addressDto.PhoneNumbers, addressDto.Emails, addressDto.Socials);
        }
    }
}
