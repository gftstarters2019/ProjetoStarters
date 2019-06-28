using System.Collections.Generic;

namespace Backend.Core.Domains
{
    public class ContractHolderDomain
    {
        public IndividualDomain Individual {get; set;}
        public List<TelephoneDomain> IndividualTelephones { get; set; }
        public List<AddressDomain> IndividualAddresses { get; set; }
    }
}
