namespace Backend.Infrastructure.Converters
{
    public sealed class ConvertersManager
    {
        private static ContractConverter contractConverter = null;
        private static SignedContractConverter signedContractConverter = null;
        private static IndividualConverter individualConverter = null;
        private static PetConverter petConverter = null;
        private static MobileDeviceConverter mobileDeviceConverter = null;
        private static RealtyConverter realtyConverter = null;
        private static VehicleConverter vehicleConverter = null;
        private static AddressConverter addressConverter = null;
        private static readonly object padlock = new object();

        public ConvertersManager()
        {
        }

        public static ContractConverter ContractConverter
        {
            get
            {
                if(contractConverter == null)
                {
                    lock(padlock)
                    {
                        if(contractConverter == null)
                        {
                            contractConverter = new ContractConverter();
                        }
                    }
                }
                return contractConverter;
            }
        }

        public static SignedContractConverter SignedContractConverter
        {
            get
            {
                if (signedContractConverter == null)
                {
                    lock (padlock)
                    {
                        if (signedContractConverter == null)
                        {
                            signedContractConverter = new SignedContractConverter();
                        }
                    }
                }
                return signedContractConverter;
            }
        }

        public static IndividualConverter IndividualConverter
        {
            get
            {
                if (individualConverter == null)
                {
                    lock (padlock)
                    {
                        if (individualConverter == null)
                        {
                            individualConverter = new IndividualConverter();
                        }
                    }
                }
                return individualConverter;
            }
        }

        public static PetConverter PetConverter
        {
            get
            {
                if (petConverter == null)
                {
                    lock (padlock)
                    {
                        if (petConverter == null)
                        {
                            petConverter = new PetConverter();
                        }
                    }
                }
                return petConverter;
            }
        }

        public static MobileDeviceConverter MobileDeviceConverter
        {
            get
            {
                if (mobileDeviceConverter == null)
                {
                    lock (padlock)
                    {
                        if (mobileDeviceConverter == null)
                        {
                            mobileDeviceConverter = new MobileDeviceConverter();
                        }
                    }
                }
                return mobileDeviceConverter;
            }
        }

        public static RealtyConverter RealtyConverter
        {
            get
            {
                if (realtyConverter == null)
                {
                    lock (padlock)
                    {
                        if (realtyConverter == null)
                        {
                            realtyConverter = new RealtyConverter();
                        }
                    }
                }
                return realtyConverter;
            }
        }

        public static VehicleConverter VehicleConverter
        {
            get
            {
                if (vehicleConverter == null)
                {
                    lock (padlock)
                    {
                        if (vehicleConverter == null)
                        {
                            vehicleConverter = new VehicleConverter();
                        }
                    }
                }
                return vehicleConverter;
            }
        }

        public static AddressConverter AddressConverter
        {
            get
            {
                if (addressConverter == null)
                {
                    lock (padlock)
                    {
                        if (addressConverter == null)
                        {
                            addressConverter = new AddressConverter();
                        }
                    }
                }
                return addressConverter;
            }
        }
    }
}
