using Backend.Application.Factories;
using Backend.Application.ViewModels;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Singleton
{
    public class ViewModelCreator
    {
        private static Individual individual = null;
        private static Address address = null;
        private static Telephone telephone = null;
        private static ContractHolderViewModel _contractHolderViewModel = null;
        private static readonly object padlock = new object();

        ViewModelCreator(ContractHolderViewModel contractHolderViewModel)
        {
            _contractHolderViewModel = contractHolderViewModel;
        }

        public static Individual Individual
        {
            get
            {
                if(individual == null)
                {
                    lock (padlock)
                    {
                        if(individual == null)
                        {
                            var individualFactory = new IndividualFactory();
                            individual = individualFactory.Create(_contractHolderViewModel);
                        }
                    }
                }
                return individual;
            }
        }

        public static Address Address
        {
            get
            {
                if (address == null)
                {
                    lock (padlock)
                    {
                        if (address == null)
                        {
                            address = new Address();
                        }
                    }
                }
                return address;
            }
        }

        public static Telephone Telephone
        {
            get
            {
                if (telephone == null)
                {
                    lock (padlock)
                    {
                        if (telephone == null)
                        {
                            telephone = new Telephone();
                        }
                    }
                }
                return telephone;
            }
        }
    }
}
