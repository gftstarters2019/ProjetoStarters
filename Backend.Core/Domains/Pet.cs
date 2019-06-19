using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class Pet : Beneficiary
    {
        public string PetName { get; set; }
        public PetSpecies PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public DateTime PetBirthdate { get; set; }
    }
}
