using Backend.Core.Domains;
using Beneficiaries.WebAPI.Factories.Interfaces;
using Beneficiaries.WebAPI.ViewModels;
using System;

namespace Beneficiaries.WebAPI.Factories
{
    public class PetViewModelFactory : IFactory<PetViewModel, PetDomain>
    {
        public PetViewModel Create(PetDomain petDomain)
        {
            if (petDomain == null)
                return null;

            return new PetViewModel()
            {
                BeneficiaryId = petDomain.BeneficiaryId,
                PetBirthdate = petDomain.PetBirthdate,
                PetBreed = petDomain.PetBreed,
                PetName = petDomain.PetName,
                PetSpecies = petDomain.PetSpecies
            };
        }
    }
}
