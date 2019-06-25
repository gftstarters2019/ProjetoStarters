using Backend.Core.Domains;
using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contract.WebAPI.ViewModels
{
    public class ContractViewModel
    {
        public Guid SignedContractId { get; set; }
        /// <summary>
        /// Contract Holder ID (BeneficiaryId)
        /// </summary>
        public Guid ContractHolderId { get; set; }
        public IndividualDomain ContractHolder { get; set; }
        public ContractType Type { get; set; }
        public ContractCategory Category { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public List<IndividualDomain> Individuals { get; set; }
        public List<RealtyViewModel> Realties { get; set; }
        public List<MobileDeviceDomain> MobileDevices { get; set; }
        public List<PetDomain> Pets { get; set; }
        public List<VehicleDomain> Vehicles { get; set; }
    }
}
