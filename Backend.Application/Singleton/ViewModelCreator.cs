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
        private static List<Address> addresses = null;
        private static List<Telephone> telephones = null;
        private static ContractHolderViewModel _contractHolderViewModel = null;
        private static readonly object padlock = new object();

        public ViewModelCreator(ContractHolderViewModel contractHolderViewModel)
        {
            _contractHolderViewModel = contractHolderViewModel;
        }

        public Individual Individual
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
                            return individualFactory.Create(_contractHolderViewModel);
                        }
                    }
                }
                return individual;
            }
        }

        public List<Telephone> Telephone
        {
            get
            {
                if (telephones == null)
                {
                    lock (padlock)
                    {
                        if (telephones == null)
                        {
                            var telephoneFactory = new TelephoneFactory();
                            return telephoneFactory.CreateList(_contractHolderViewModel.IndividualTelephones);
                        }
                    }
                }
                return telephones;
            }
        }

        //public static Address Address
        //{
        //    get
        //    {
        //        if (address == null)
        //        {
        //            lock (padlock)
        //            {
        //                if (address == null)
        //                {
        //                    address = new Address();
        //                }
        //            }
        //        }
        //        return address;
        //    }
        //}
    }
}
