using System;
using System.Globalization;

namespace backend.Model
{
    /// <summary>
    /// Database table.
    /// For data exchange.
    /// Aka Entity.
    /// Aka Model.
    /// </summary>
    public sealed class Address
    {
        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed dependencies.
        /// </summary>
        /// <param name="street">A given street.</param>
        /// <param name="postalCode">A given postal code.</param>
        /// <param name="city">A given city.</param>
        /// <param name="country">A given country.</param>
        /// <param name="phoneNumbers">A given list of phone numbers.</param>
        /// <param name="emails">A given list of emails.</param>
        /// <param name="socials">A given list of social networks.</param>
        public Address(string street, string postalCode, string city, string country, 
            string phoneNumbers, string emails, string socials)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(street));
            if(!string.IsNullOrEmpty(country)) Country = country;
            PhoneNumbers = phoneNumbers;
            Emails = emails;
            Socials = socials;
        }

        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed dependencies.
        /// </summary>
        /// <param name="addressId">A given address id.</param>
        /// <param name="street">A given street.</param>
        /// <param name="postalCode">A given postal code.</param>
        /// <param name="city">A given city.</param>
        /// <param name="country">A given country.</param>
        /// <param name="phoneNumbers">A given list of phone numbers.</param>
        /// <param name="emails">A given list of emails.</param>
        /// <param name="socials">A given list of social networks.</param>
        public Address(Guid addressId, string street, string postalCode, string city, string country,
            string phoneNumbers, string emails, string socials) : this(street, postalCode, city, country, phoneNumbers,
            emails, socials)
        {
            AddressId = addressId;
        }

        /// <summary>
        /// The given property.
        /// </summary>
        public Guid AddressId { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string Street { get; set; }
        
        /// <summary>
        /// The given property.
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// The given property.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string Country { get; set; } = CultureInfo.CurrentCulture.ThreeLetterISOLanguageName;

        /// <summary>
        /// The given property.
        /// </summary>
        public string PhoneNumbers { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string Emails { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string Socials { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public Address()
        {
        }

        /// <summary>
        /// Readable display of the current instance.
        /// </summary>
        /// <returns>string.</returns>
        public override string ToString()
        {
            return string.Format("{0}, {1} {2}", Street, PostalCode, City);
        }
    }
}
