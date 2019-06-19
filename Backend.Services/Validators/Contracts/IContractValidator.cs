using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IContractValidator
    {
        bool IsValid(Contract contract, List<Individual> individuals, List<MobileDevice> mobileDevices, List<Pet> pets, List<Realty> realties, List<Vehicle> vehicles);
    }
}
