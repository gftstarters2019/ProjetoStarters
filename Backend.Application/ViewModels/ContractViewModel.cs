using Backend.Core.Enums;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ViewModels
{
    public class ContractViewModel
    {
        public Guid SignedContractId { get; set; }
        /// <summary>
        /// Contract Holder ID (BeneficiaryId)
        /// </summary>
        public Guid ContractHolderId { get; set; }
        public IndividualEntity ContractHolder { get; set; }
        public ContractType Type { get; set; }
        public ContractCategory Category { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// List of Beneficiaries. Shouldn't have different types of Beneficiary, but can accept any type.
        /// </summary>
        public List<Guid> BeneficiariesIds { get; set; }

        public List<IndividualEntity> Individuals { get; set; }
        public List<RealtyViewModel> Realties { get; set; }
        public List<MobileDeviceEntity> MobileDevices { get; set; }
        public List<PetEntity> Pets { get; set; }
        public List<VehicleEntity> Vehicles { get; set; }
    }
}
