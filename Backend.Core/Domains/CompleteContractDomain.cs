using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class CompleteContractDomain
    {
        public ContractDomain Contract { get; set; }
        public SignedContractDomain SignedContract { get; set; }
        public List<IndividualDomain> Individuals { get; set; }
        public List<PetDomain> Pets { get; set; }
        public List<MobileDeviceDomain> MobileDevices { get; set; }
        public List<RealtyDomain> Realties { get; set; }
        public List<VehicleDomain> Vehicles { get; set; }
    }
}
