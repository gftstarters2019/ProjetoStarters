﻿using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beneficiaries.WebAPI.ViewModels
{
    public class PetViewModel
    {
        public Guid BeneficiaryId { get; set; }
        [MaxLength(40)]
        public string PetName { get; set; }
        public PetSpecies PetSpecies { get; set; }
        [MaxLength(30)]
        public string PetBreed { get; set; }
        public DateTime PetBirthdate { get; set; }
    }
}
