using Backend.Core.Models;
using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Services.Validators
{
    public class TelephoneValidator : ITelephoneValidator
    {
        public bool IsValid(Telephone telephone)
        {
            telephone.TelephoneNumber = Regex.Replace(telephone.TelephoneNumber, "\\D+", "");
            if (telephone.TelephoneNumber.Length < 10)
                return false;
            return true;
        }
    }
}
