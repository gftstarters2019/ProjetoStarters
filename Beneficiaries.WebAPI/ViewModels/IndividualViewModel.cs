﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Beneficiaries.WebAPI.ViewModels
{
    public class IndividualViewModel
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
    }
}
