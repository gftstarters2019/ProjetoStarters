using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class Individual
    {
        public string IndividualName { get; set; }
        public string IndividualCPF { get; set; }
        public string IndividualRG { get; set; }
        public string IndividualEmail { get; set; }
        public DateTime IndividualBirthdate { get; set; }
    }
}
