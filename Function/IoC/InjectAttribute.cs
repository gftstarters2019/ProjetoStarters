﻿using Microsoft.Azure.WebJobs.Description;
using System;

namespace Function.IoC
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}
