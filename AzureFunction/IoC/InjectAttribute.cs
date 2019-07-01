using Microsoft.Azure.WebJobs.Description;
using System;

namespace AzureFunction.IoC
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}
