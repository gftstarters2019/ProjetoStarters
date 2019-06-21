﻿using Backend.Core.Models;
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

        public bool IsValid(Contract contract, List<Individual> individuals, List<MobileDevice> mobileDevices, List<Pet> pets, List<Realty> realties, List<Vehicle> vehicles)
        {
            if (individuals != null)
            {
                foreach (var item in individuals)
                {
                    if (!_individualValidator.IsValid(item))
                        return false;
                }
            }

            if (mobileDevices != null)
            {
                foreach (var item in mobileDevices)
                {
                    if (!_mobileDeviceValidator.IsValid(item))
                        return false;
                }
            }

            if (pets != null)
            {
                foreach (var item in pets)
                {
                    if (!_petValidator.IsValid(item))
                        return false;
                }
            }

            if (realties != null)
            {
                foreach (var item in realties)
                {
                    if (!_realtyValidator.IsValid(item))
                        return false;
                }
            }

            if (vehicles != null)
            {
                foreach (var item in vehicles)
                {
                    if (!_vehicleValidator.IsValid(item))
                        return false;
                }
            }

            return true;
        }
    }
}