namespace Contract.WebAPI.Factories
{
    public sealed class FactoriesManager
    {
        private static CompleteContractDomainFactory completeContractDomainFactory = null;
        private static ContractViewModelFactory contractViewModelFactory = null;
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
    }
}
