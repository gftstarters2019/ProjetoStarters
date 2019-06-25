namespace Beneficiaries.WebAPI.Factories
{
    public sealed class FactoriesManager
    {
        private static IndividualViewModelFactory individualViewModelFactory = null;
        private static MobileDeviceViewModelFactory mobileDeviceViewModelFactory = null;
        private static PetViewModelFactory petViewModelFactory = null;
        private static RealtyViewModelFactory realtyViewModelFactory = null;
        private static VehicleViewModelFactory vehicleViewModelFactory = null;
        private static readonly object padlock = new object();

        public FactoriesManager()
        {
        }

        public static IndividualViewModelFactory IndividualViewModel
        {
            get
            {
                if (individualViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (individualViewModelFactory == null)
                        {
                            individualViewModelFactory = new IndividualViewModelFactory();
                        }
                    }
                }
                return individualViewModelFactory;
            }
        }

        public static MobileDeviceViewModelFactory MobileDeviceViewModel
        {
            get
            {
                if (mobileDeviceViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (mobileDeviceViewModelFactory == null)
                        {
                            mobileDeviceViewModelFactory = new MobileDeviceViewModelFactory();
                        }
                    }
                }
                return mobileDeviceViewModelFactory;
            }
        }

        public static PetViewModelFactory PetViewModel
        {
            get
            {
                if (petViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (petViewModelFactory == null)
                        {
                            petViewModelFactory = new PetViewModelFactory();
                        }
                    }
                }
                return petViewModelFactory;
            }
        }

        public static RealtyViewModelFactory RealtyViewModel
        {
            get
            {
                if (realtyViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (realtyViewModelFactory == null)
                        {
                            realtyViewModelFactory = new RealtyViewModelFactory();
                        }
                    }
                }
                return realtyViewModelFactory;
            }
        }

        public static VehicleViewModelFactory VehicleViewModel
        {
            get
            {
                if (vehicleViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (vehicleViewModelFactory == null)
                        {
                            vehicleViewModelFactory = new VehicleViewModelFactory();
                        }
                    }
                }
                return vehicleViewModelFactory;
            }
        }
    }
}
