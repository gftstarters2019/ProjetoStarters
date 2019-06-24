namespace ContractHolder.WebAPI.Factories
{
    public class FactoriesManager
    {
        private static ContractHolderFactory contractHolderFactory = null;
        private static IndividualDomainFactory individualDomainFactory = null;
        private static AddressDomainListFactory addressDomainListFactory = null;
        private static TelephoneDomainListFactory telephoneDomainListFactory = null;
        private static readonly object padlock = new object();

        public FactoriesManager()
        {
        }

        public static ContractHolderFactory ContractHolder
        {
            get
            {
                if (contractHolderFactory == null)
                {
                    lock (padlock)
                    {
                        if (contractHolderFactory == null)
                        {
                            contractHolderFactory = new ContractHolderFactory();
                        }
                    }
                }
                return contractHolderFactory;
            }
        }

        public static IndividualDomainFactory IndividualDomain
        {
            get
            {
                if (individualDomainFactory == null)
                {
                    lock (padlock)
                    {
                        if (individualDomainFactory == null)
                        {
                            individualDomainFactory = new IndividualDomainFactory();
                        }
                    }
                }
                return individualDomainFactory;
            }
        }

        public static AddressDomainListFactory AddressDomainList
        {
            get
            {
                if (addressDomainListFactory == null)
                {
                    lock (padlock)
                    {
                        if (addressDomainListFactory == null)
                        {
                            addressDomainListFactory = new AddressDomainListFactory();
                        }
                    }
                }
                return addressDomainListFactory;
            }
        }

        public static TelephoneDomainListFactory TelephoneDomainList
        {
            get
            {
                if (telephoneDomainListFactory == null)
                {
                    lock (padlock)
                    {
                        if (telephoneDomainListFactory == null)
                        {
                            telephoneDomainListFactory = new TelephoneDomainListFactory();
                        }
                    }
                }
                return telephoneDomainListFactory;
            }
        }
    }
}
