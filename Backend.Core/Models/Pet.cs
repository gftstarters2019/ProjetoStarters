using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class Pet : Beneficiary
    {
        public Guid PetId { get; set; }
        [MaxLength(40)]
        public string PetName { get; set; }
        [MaxLength(25)]
        public PetSpecies PetSpecies { get; set; }
        [MaxLength(30)]
        public string PetBreed { get; set; }
        public DateTime PetBirthdate { get; set; }
    }
}
