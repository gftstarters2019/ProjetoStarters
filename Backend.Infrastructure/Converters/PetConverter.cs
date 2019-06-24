using Backend.Core.Domains;
using Backend.Core.Models;
using Backend.Infrastructure.Converters.Interfaces;

namespace Backend.Infrastructure.Converters
{
    public class PetConverter : IConverter<PetDomain, PetEntity>
    {
        public PetDomain Convert(PetEntity petEntity)
        {
            if (petEntity == null)

                return null;
            var petDomain = new PetDomain()
            {
                BeneficiaryId = petEntity.BeneficiaryId,
                IsDeleted = petEntity.IsDeleted,
                PetBirthdate = petEntity.PetBirthdate,
                PetBreed = petEntity.PetBreed,
                PetName = petEntity.PetName,
                PetSpecies = petEntity.PetSpecies
            };

            return petDomain;
        }

        public PetEntity Convert(PetDomain petDomain)
        {
            if (petDomain == null)
                return null;

            var petEntity = new PetEntity()
            {
                BeneficiaryId = petDomain.BeneficiaryId,
                IsDeleted = petDomain.IsDeleted,
                PetBirthdate = petDomain.PetBirthdate,
                PetBreed = petDomain.PetBreed,
                PetName = petDomain.PetName,
                PetSpecies = petDomain.PetSpecies
            };

            return petEntity;
        }
    }
}
