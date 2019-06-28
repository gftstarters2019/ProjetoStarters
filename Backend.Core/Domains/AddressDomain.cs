using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class AddressDomain
    {
        public Guid AddressId { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
