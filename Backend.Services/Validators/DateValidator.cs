using Backend.Services.Validators.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators
{
    public class DateValidator : IDateValidator
    {
        public List<string> IsValid(DateTime date)
        {
            var errors = new List<string>();

            if (date == null)
                errors.Add("Data Vazia! ");
            else if (date > DateTime.Today)
                errors.Add($"{date}: Data Inválida! ");

            return errors;

        }
        public List<string> IsOfAge(DateTime date)
        {
            var errors = new List<string>();

            if (date.Year - DateTime.Today.Year < 18)
                errors.Add($"{date}: Pessoa não é maior de idade!");
            return errors;
        }
    }
}
