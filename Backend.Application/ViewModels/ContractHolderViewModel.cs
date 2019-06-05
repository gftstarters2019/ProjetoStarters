using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.ViewModels
{
    public class ContractHolderViewModel
    {
        public ContractHolderViewModel(){
            IndividualAddresses = new List<Address>();
            IndividualTelephones = new List<Telephone>();
        }

        public Guid IndividualId { get; set; }
        [MaxLength(50)]
        public string IndividualName { get; set; }
        [MaxLength(11)]
        public string IndividualCPF { get; set; }
        [MaxLength(9)]
        public string IndividualRG { get; set; }
        [MaxLength(30)]
        public string IndividualEmail { get; set; }
        public DateTime IndividualBirthdate { get; set; }

        public List<Telephone> IndividualTelephones { get; set; }
        public List<Address> IndividualAddresses { get; set; }
    }
}