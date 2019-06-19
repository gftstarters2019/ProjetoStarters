using ContractHolder.WebAPI.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractHolder.WebAPI.Singleton
{
    public sealed class Factories
    {
        private static IndividualFactory individualFactory = null;
        private static TelephoneFactory telephoneFactory = null;
        private static AddressFactory addressFactory = null;
        private static readonly object padlock = new object();

        public Factories()
        {
        }

        public static IndividualFactory IndividualFactory
        {
            get
            {
                if (individualFactory == null)
                {
                    lock (padlock)
                    {
                        if (individualFactory == null)
                        {
                            individualFactory = new IndividualFactory();
                        }
                    }
                }
                return individualFactory;
            }
        }

        public static TelephoneFactory TelephoneFactory
        {
            get
            {
                if (telephoneFactory == null)
                {
                    lock (padlock)
                    {
                        if (telephoneFactory == null)
                        {
                            telephoneFactory = new TelephoneFactory();
                        }
                    }
                }
                return telephoneFactory;
            }
        }

        public static AddressFactory AddressFactory
        {
            get
            {
                if (addressFactory == null)
                {
                    lock (padlock)
                    {
                        if (addressFactory == null)
                        {
                            addressFactory = new AddressFactory();
                        }
                    }
                }
                return addressFactory;
            }
        }
    }
}
