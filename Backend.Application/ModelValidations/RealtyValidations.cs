using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ModelValidations
{
    public static class RealtyValidations
    {
        public static bool RealtyIsValid(RealtyEntity realty)
        {
            if (realty.RealtyMarketValue < 0 && realty.RealtySaleValue < 0)
                return false;

            //if (!CEPIsValid(realty.RealtyAddress.AddressZipCode))
            //    return false;

            if (!ValidationsHelper.DateIsValid(realty.RealtyConstructionDate))
                return false;

            return true;
        }

        public static bool CEPIsValid(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }
            return System.Text.RegularExpressions.Regex.IsMatch(cep, "[0-9]{5}-[0-9]{3}");
        }
    }
}
