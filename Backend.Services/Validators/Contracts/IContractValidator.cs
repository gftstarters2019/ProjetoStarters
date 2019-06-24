using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IContractValidator
    {
        bool IsValid(ContractDomain contract, List<IndividualDomain> individuals, List<MobileDeviceDomain> mobileDevices, List<PetDomain> pets, List<RealtyDomain> realties, List<VehicleDomain> vehicles);
    }
}
