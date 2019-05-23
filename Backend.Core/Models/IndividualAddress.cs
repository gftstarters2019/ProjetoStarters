using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class IndividualAddress
    {
        public Guid IndividualAddressId { get; set; }
        public Guid AddressId { get; set; }
        public Guid IndividualId { get; set; }
        public Individual Individual { get; set; }
        public Address Address { get; set; }
    }
}
