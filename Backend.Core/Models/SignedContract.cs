﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class SignedContract
    {
        [Key]
        public Guid ContractSignedId { get; set; }
        public Guid IndividualId { get; set; }
        public Guid ContractId { get; set; }
        public Individual ContractSignedIndividual { get; set; }
        public Contract ContractSignedContract { get; set; }

        public bool ContractIndividualIsActive { get; set; }
    }
}