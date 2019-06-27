using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Backend.Services.Validators
{
    public class NumberValidator : INumberValidator
    {
        public bool IsPositive(string number)
        {
            number = NumberUnmask(number);
            double numberConverted;
            double.TryParse(number, out numberConverted);
            if (numberConverted < 0.0)
                return false;
            return true;
        }

        public bool LengthValidator(string number, int length)
        {
            number = NumberUnmask(number);
            if (number.Length < length)
                return false;
            return true;
        }

        private string NumberUnmask(string number)
        {
            number = Regex.Replace(number, "\\D+", "");
            return number;
        }
    }
}
