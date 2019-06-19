using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class ContractValidator: IContractValidator
    {
        private readonly IIndividualValidator _individualValidator;
        private readonly IMobileDeviceValidator _mobileDeviceValidator;
        private readonly IPetValidator _petValidator;
        private readonly IRealtyValidator _realtyValidator;
        private readonly IVehicleValidator _vehicleValidator;

        public ContractValidator(IIndividualValidator individualValidator, IMobileDeviceValidator mobileDeviceValidator, INumberValidator numberValidator, IPetValidator petValidator, IRealtyValidator realtyValidator, IVehicleValidator vehicleValidator)
        {
            _individualValidator = individualValidator;
            _mobileDeviceValidator = mobileDeviceValidator;
            _petValidator = petValidator;
            _realtyValidator = realtyValidator;
            _vehicleValidator = vehicleValidator;
        }

        public bool IsValid(Contract contract, List<Individual> individuals)
        {
            foreach(var item in individuals)
            {
                if (!_individualValidator.IsValid(item))
                    return false;
            }
            return true;
        }

        public bool IsValid(Contract contract, List<MobileDevice> mobileDevices)
        {
            foreach (var item in mobileDevices)
            {
                if (!_mobileDeviceValidator.IsValid(item))
                    return false;
            }
            return true;
        }

        public bool IsValid(Contract contract, List<Pet> pets)
        {
            foreach (var item in pets)
            {
                if (!_petValidator.IsValid(item))
                    return false;
            }
            return true;
        }

        public bool IsValid(Contract contract, List<Realty> realties)
        {
            foreach (var item in realties)
            {
                if (!_realtyValidator.IsValid(item))
                    return false;
            }
            return true;
        }

        public bool IsValid(Contract contract, List<Vehicle> vehicles)
        {
            foreach (var item in vehicles)
            {
                if (!_vehicleValidator.IsValid(item))
                    return false;
            }
            return true;
        }
    }
}
