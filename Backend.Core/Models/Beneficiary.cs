using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public abstract class Beneficiary
    {
        public Guid BeneficiaryId { get; set; }
    }
}