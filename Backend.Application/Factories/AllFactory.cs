using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Factories
{
    class AllFactory
    {
        public dynamic Create(string factoryName)
        {
            switch (factoryName)
            {
                case "individual":
                    return new IndividualFactory();
                case "Contract":
                    return new ContractFactory();
                case "contractSigned":
                    return new SignedContractFactory();
                case "pet":
                    return new PetFactory();
                case "realty":
                    return new RealtyFactory();
                case "vehicle":
                    return new vehicleFactory();
                case "mobileDevice":
                    return new MobileDeviceFactory();
                case "contractBeneficiary":
                    return new ContractBeneficiaryFactory();
                default:
                    return null;
            }
        }
    }
}
