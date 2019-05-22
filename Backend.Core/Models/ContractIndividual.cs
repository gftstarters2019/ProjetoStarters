using Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class ContractIndividual
    {
        public Guid ContractIndividualId { get; set; }
        public List<Individual> ContractIndividuals { get; set; }
        public List<Guid> ContractIndividualsId { get; set; }
        public List<Contract> ContractContracts { get; set; }
        public List<Guid> ContractContractsId { get; set; }
        public List<IBeneficiary> ContractBeneficiaries { get; set; }
        public List<Guid> ContractBeneficiariesId { get; set; }
        public Boolean ContractIndividualIsActive { get; set; }
    }
}
