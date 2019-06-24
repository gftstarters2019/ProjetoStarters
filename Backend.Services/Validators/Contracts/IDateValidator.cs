using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IDateValidator
    {
        bool IsValid(DateTime date);
        bool IsOfAge(DateTime date);
    }
}
