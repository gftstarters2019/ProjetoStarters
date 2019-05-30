using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Helper
{
    public class HolderProxy
    {
        private Individual _individual = new Individual();
        
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
        public bool IndividualDeleted { get; set; }
        private ICollection<Address> _address;
        private ICollection<Telephone> _telephone;




    }
} 

