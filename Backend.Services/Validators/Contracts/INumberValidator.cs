using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface INumberValidator
    {
        List<string> IsPositive(string number);
        List<string> LengthValidator(string number, int length);
    }
}
