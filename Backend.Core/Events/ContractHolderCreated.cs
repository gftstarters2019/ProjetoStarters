using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Events
{
    public class ContractHolderCreated
    {
        public ContractHolderCreated(Individual individual)
        {
            ContractHolder = individual;
        }

        public Individual ContractHolder { get; set; }
    }
}
