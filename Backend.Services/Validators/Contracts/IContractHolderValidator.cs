using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IContractHolderValidator
    {
        bool IsValid(Individual individual, List<Address> addresses, List<Telephone> telephones);
    }
}
