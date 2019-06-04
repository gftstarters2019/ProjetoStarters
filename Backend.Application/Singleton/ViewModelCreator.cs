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
        private static ContractHolderViewModel _contractHolderViewModel = new ContractHolderViewModel();
        private static readonly object padlock = new object();

        public ViewModelCreator(ContractHolderViewModel contractHolderViewModel)
        {
            //_contractHolderViewModel = new ContractHolderViewModel();
            _contractHolderViewModel = contractHolderViewModel;

            //_contractHolderViewModel.IndividualCPF = contractHolderViewModel.IndividualCPF;
            //_contractHolderViewModel.IndividualBirthdate = contractHolderViewModel.IndividualBirthdate;
            //_contractHolderViewModel.IndividualEmail = contractHolderViewModel.IndividualEmail;
            //_contractHolderViewModel.IndividualName = contractHolderViewModel.IndividualName;
            //_contractHolderViewModel.IndividualRG = contractHolderViewModel.IndividualRG;
            //foreach (var tel in contractHolderViewModel.IndividualTelephones)
            //{
            //    Telephone telephone = new Telephone();
            //    telephone.TelephoneId = Guid.NewGuid();
            //    telephone.TelephoneNumber = tel.TelephoneNumber;
            //    telephone.TelephoneType = tel.TelephoneType;

            //    _contractHolderViewModel.IndividualTelephones.Add(telephone);
            //}
            //foreach (var ad in contractHolderViewModel.IndividualAddresses)
            //{
            //    Address address = new Address();
            //    address.AddressCity = ad.AddressCity;
            //    address.AddressComplement = ad.AddressComplement;
            //    address.AddressCountry = ad.AddressCountry;
            //    address.AddressNeighborhood = ad.AddressNeighborhood;
            //    address.AddressNumber = ad.AddressNumber;
            //    address.AddressState = ad.AddressState;
            //    address.AddressStreet = ad.AddressStreet;
            //    address.AddressType = ad.AddressType;
            //    address.AddressZipCode = ad.AddressZipCode;

            //    _contractHolderViewModel.IndividualAddresses.Add(address);
            //}
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

        public List<Address> Address
        {
            get
            {
                if (addresses == null)
                {
                    lock (padlock)
                    {
                        if (addresses == null)
                        {
                            var addressFactory = new AddressFactory();
                            return addressFactory.CreateList(_contractHolderViewModel.IndividualAddresses);
                        }
                    }
                }
                return addresses;
            }
        }
    }
}
