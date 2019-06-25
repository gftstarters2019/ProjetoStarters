namespace Contract.WebAPI.Factories
{
    public sealed class FactoriesManager
    {
        private static CompleteContractDomainFactory completeContractDomainFactory = null;
        private static ContractViewModelFactory contractViewModelFactory = null;
        private static RealtyViewModelListFactory realtyViewModelListFactory = null;
        private static RealtyDomainListFactory realtyDomainListFactory = null;
        private static readonly object padlock = new object();

        public FactoriesManager()
        {
        }

        public static CompleteContractDomainFactory CompleteContractDomain
        {
            get
            {
                if (completeContractDomainFactory == null)
                {
                    lock (padlock)
                    {
                        if (completeContractDomainFactory == null)
                        {
                            completeContractDomainFactory = new CompleteContractDomainFactory();
                        }
                    }
                }
                return completeContractDomainFactory;
            }
        }

        public static ContractViewModelFactory ContractViewModel
        {
            get
            {
                if (contractViewModelFactory == null)
                {
                    lock (padlock)
                    {
                        if (contractViewModelFactory == null)
                        {
                            contractViewModelFactory = new ContractViewModelFactory();
                        }
                    }
                }
                return contractViewModelFactory;
            }
        }

        public static RealtyViewModelListFactory RealtyViewModelList
        {
            get
            {
                if (realtyViewModelListFactory == null)
                {
                    lock (padlock)
                    {
                        if (realtyViewModelListFactory == null)
                        {
                            realtyViewModelListFactory = new RealtyViewModelListFactory();
                        }
                    }
                }
                return realtyViewModelListFactory;
            }
        }

        public static RealtyDomainListFactory RealtyDomainList
        {
            get
            {
                if (realtyDomainListFactory == null)
                {
                    lock (padlock)
                    {
                        if (realtyDomainListFactory == null)
                        {
                            realtyDomainListFactory = new RealtyDomainListFactory();
                        }
                    }
                }
                return realtyDomainListFactory;
            }
        }
    }
}
