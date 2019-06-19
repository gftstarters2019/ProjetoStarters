using Backend.Core.Models;
using ContractHolder.WebAPI.Factories.Interfaces;
using System;
using System.Collections.Generic;

namespace ContractHolder.WebAPI.Factories
{
    public class TelephoneFactory : IFactoryList<Telephone>
    {
        private List<Telephone> telephones = null;
        private Telephone telephone = null;

        public TelephoneFactory()
        {
            telephones = new List<Telephone>();
        }

        public List<Telephone> CreateList(List<Telephone> vm_telephones)
        {
            telephones = new List<Telephone>();
            foreach (var tel in vm_telephones)
            {
                telephone = new Telephone();

                telephone.TelephoneId = Guid.NewGuid();
                telephone.TelephoneNumber = tel.TelephoneNumber;
                telephone.TelephoneType = tel.TelephoneType;

                telephones.Add(telephone);
            }

            if (telephones.Count != vm_telephones.Count || telephones.Count > 5)
            {
                return null;
            }

            return telephones;
        }
    }
}
