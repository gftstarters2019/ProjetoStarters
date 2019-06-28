using System;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IDateValidator
    {
        List<string> IsValid(DateTime date);
        List<string> IsOfAge(DateTime date);
    }
}
