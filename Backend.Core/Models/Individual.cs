using Backend.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class Individual : Beneficiary , IvalidatorIndividual
    {
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

        public void ValidateCPF()
        {
            throw new NotImplementedException();
        }

        public void ValidateDate()
        {
            throw new NotImplementedException();
        }

        public void ValidateRG()
        {
            throw new NotImplementedException();
        }
    }
}
