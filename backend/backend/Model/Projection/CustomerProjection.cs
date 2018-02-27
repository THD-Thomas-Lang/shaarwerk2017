using System;
using System.Collections.Generic;

namespace backend.Model.Projection
{
    /// <summary>
    /// Database table.
    /// For data exchange.
    /// Aka Entity.
    /// Aka Model.
    /// </summary>
    public sealed class CustomerProjection
    {

        /// <summary>
        /// The given property.
        /// </summary>
        public Guid CustomerId { get; set; }

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
        public string ShortAddress { get; set; }
        
    }
}
