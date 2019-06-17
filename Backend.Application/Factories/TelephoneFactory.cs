using Backend.Application.Factories.Interfaces;
using Backend.Core.Enums;
using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Backend.Application.Factories
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
                if (Validate(tel))
                {
                    telephone = new Telephone();

                    telephone.TelephoneId = Guid.NewGuid();
                    telephone.TelephoneNumber = tel.TelephoneNumber;
                    telephone.TelephoneType = tel.TelephoneType;

                    telephones.Add(telephone);
                }
            }

            if(telephones.Count != vm_telephones.Count  || telephones.Count > 5)
            {
                return null;
                //telephones.Clear();
                //return telephones;
            }

            return telephones;
        }

        /// <summary>
        /// Validações de Telephone
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        private bool Validate(Telephone telephone)
        {
            Regex regex = new Regex("^[0-9]+$");

            if (!regex.IsMatch(telephone.TelephoneNumber))
                return false;

            if (telephone.TelephoneNumber.Length < 8 || telephone.TelephoneNumber.Length > 11)
                return false;

            if (!Enum.IsDefined(typeof(TelephoneType), telephone.TelephoneType))
                return false;

            return true;
        }
    }
}
