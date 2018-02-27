using System;
using System.Collections.Generic;

namespace backend.Model
{
    /// <summary>
    /// Database table.
    /// For data exchange.
    /// Aka Entity.
    /// Aka Model.
    /// </summary>
    public sealed class Customer
    {
        /// <summary>
        /// Overloaded constructor.
        /// Builds up the needed dependencies.
        /// </summary>
        /// <param name="salutation">A given salutation.</param>
        /// <param name="firstName">A given firstname.</param>
        /// <param name="lastName">A given lastname.</param>
        /// <param name="birthDay">A given birthday.</param>
        /// <param name="memo">A given memo.</param>
        public Customer(string salutation, string firstName, string lastName, DateTime? birthDay,
            string memo)
        {
            Salutation = salutation ?? throw new ArgumentNullException(nameof(salutation));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            BirthDay = birthDay;
            Memo = memo;
        }

        /// <inheritdoc />
        /// <summary>
        /// Overloaded constructor.
        /// Builds the needed references.
        /// </summary>
        /// <param name="customerId">A given customer id.</param>
        /// <param name="number">A given number.</param>
        /// <param name="salutation">A given salutation.</param>
        /// <param name="firstName">A given firstname.</param>
        /// <param name="lastName">A given lastname.</param>
        /// <param name="birthDay">A given birthday.</param>
        /// <param name="memo">A given memo.</param>
        public Customer(Guid customerId, short number, string salutation, string firstName, string lastName, DateTime? birthDay,
            string memo) : this(salutation, firstName, lastName, birthDay, memo)
        {
            CustomerId = customerId;
            Number = number;
        }

        /// <summary>
        /// The given property.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string Salutation { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public short Number { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// The given property.
        /// </summary>
        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        /// <summary>
        /// The given property.
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public Customer()
        {
        }
        
    }
}
