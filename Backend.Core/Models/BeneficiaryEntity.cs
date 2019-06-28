using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public abstract class BeneficiaryEntity
    {
        [Key]
        public Guid BeneficiaryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}