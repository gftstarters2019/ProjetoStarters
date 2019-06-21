﻿using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Services.Validators.Contracts
{
    public interface IRealtyValidator
    {
        bool IsValid(Realty realty);
    }
}