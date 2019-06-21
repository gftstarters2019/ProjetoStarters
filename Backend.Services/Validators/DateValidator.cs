using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class DateValidator : IDateValidator
    {
        public bool IsValid(DateTime date)
        {
            return date != null ? date <= DateTime.Today : false;

        }
        public bool IsOfAge(DateTime date)
        {
            if (date.Year - DateTime.Today.Year < 18)
                return false;
            return true;
        }
    }
}
