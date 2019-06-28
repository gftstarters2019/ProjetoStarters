using Backend.Core.Domains;
using Backend.Services.Validators.Contracts;
using System.Collections.Generic;

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

        public List<string> IsValid(ContractDomain contract, List<IndividualDomain> individuals, List<MobileDeviceDomain> mobileDevices, List<PetDomain> pets, List<RealtyDomain> realties, List<VehicleDomain> vehicles)
        {
            List<string> errors = new List<string>();

            if (individuals != null)
            {
                foreach (var item in individuals)
                {
                    errors.AddRange(_individualValidator.IsValid(item));
                }
            }

            if (mobileDevices != null)
            {
                foreach (var item in mobileDevices)
                {
                    errors.AddRange(_mobileDeviceValidator.IsValid(item));
                }
            }

            if (pets != null)
            {
                foreach (var item in pets)
                {
                    errors.AddRange(_petValidator.IsValid(item));
                }
            }

            if (realties != null)
            {
                foreach (var item in realties)
                {
                    errors.AddRange(_realtyValidator.IsValid(item));
                }
            }

            if (vehicles != null)
            {
                foreach (var item in vehicles)
                {
                    errors.AddRange(_vehicleValidator.IsValid(item));
                }
            }

            return errors;
        }
    }
}
