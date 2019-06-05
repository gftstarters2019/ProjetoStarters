using Backend.Application.Factories;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Singleton
{
    public sealed class ViewModelCreator
    {
        private static IndividualFactory individualFactory = null;
        private static TelephoneFactory telephoneFactory = null;
        private static AddressFactory addressFactory = null;
        private static readonly object padlock = new object();

        public ViewModelCreator()
        {
        }

        public static IndividualFactory IndividualFactory
        {
            get
            {
                if(individualFactory == null)
                {
                    lock (padlock)
                    {
                        if(individualFactory == null)
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
