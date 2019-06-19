using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IIndividualValidator
    {
        bool CPFIsValid(string cpf);
        bool NameIsValid(string name);
        bool EmailIsValid(string emailaddress);
        bool RGIsValid(string rg);
    }
}
