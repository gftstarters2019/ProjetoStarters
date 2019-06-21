using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface INumberValidator
    {
        bool IsPositive(string number);
        bool LengthValidator(string number, int length);
    }
}
