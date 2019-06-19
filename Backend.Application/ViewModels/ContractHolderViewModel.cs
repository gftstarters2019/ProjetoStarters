using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Application.ViewModels
{
    public class ContractHolderViewModel
    {
        public ContractHolderViewModel(){
            individualAddresses = new List<AddressEntity>();
            individualTelephones = new List<Telephone>();
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

        public List<Telephone> individualTelephones { get; set; }
        public List<AddressEntity> individualAddresses { get; set; }
    }
}