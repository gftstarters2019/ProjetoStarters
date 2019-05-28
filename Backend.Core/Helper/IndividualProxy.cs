using Backend.Application.Interfaces;
using Backend.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Helper
{
    public class IndividualProxy : Beneficiary, IValidator
    {
        public Individual _individual;

        public Guid IndividualId { get; set; }
        [MaxLength(50)]
        public string IndividualName { get; set; }
        [MaxLength(11)]
        public string IndividualCPF { get; set; }
        [MaxLength(9)]
        public string IndividualRG { get; set; }

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
