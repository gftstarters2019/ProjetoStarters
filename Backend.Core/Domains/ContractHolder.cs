using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class ContractHolder
    {
        public Individual Individual {get; set;}
        public List<Telephone> IndividualTelephones { get; set; }
        public List<Address> IndividualAddresses { get; set; }
    }

}
