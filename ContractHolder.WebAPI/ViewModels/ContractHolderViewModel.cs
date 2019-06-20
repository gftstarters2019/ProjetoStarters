using Backend.Core.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContractHolder.WebAPI.ViewModels
{
    public class ContractHolderViewModel
    {
        public ContractHolderViewModel()
        {
            individualAddresses = new List<AddressDomain>();
            individualTelephones = new List<TelephoneDomain>();
        }

        public Guid individualId { get; set; }
        [MaxLength(50)]
        public string individualName { get; set; }
        [MaxLength(11)]
        public string individualCPF { get; set; }
        [MaxLength(9)]
        public string individualRG { get; set; }
        [MaxLength(30)]
        public string individualEmail { get; set; }
        public DateTime individualBirthdate { get; set; }
        public bool isDeleted { get; set; }

        public List<TelephoneDomain> individualTelephones { get; set; }
        public List<AddressDomain> individualAddresses { get; set; }
    }
}
