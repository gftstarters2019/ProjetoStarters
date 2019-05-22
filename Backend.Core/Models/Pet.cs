using Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class Pet : IBeneficiary
    {
        public Guid PetId { get; set; }
        public Guid BeneficiaryId { get; set; }
        public IBeneficiary Beneficiary { get; set; }
        public string PetName { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public DateTime PetBirthdate { get; set; }
    }
}
