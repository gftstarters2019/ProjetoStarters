using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class ContractHolderDomain
    {
        public IndividualDomain Individual {get; set;}
        public List<TelephoneDomain> IndividualTelephones { get; set; }
        public List<AddressDomain> IndividualAddresses { get; set; }
    }

}
