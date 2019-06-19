using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IContractValidator
    {
        bool IsValid(Contract contract, List<Individual> individuals);
        bool IsValid(Contract contract, List<MobileDevice> mobileDevices);
        bool IsValid(Contract contract, List<Pet> pets);
        bool IsValid(Contract contract, List<Realty> realties);
        bool IsValid(Contract contract, List<Vehicle> vehicles);
    }
}
