using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class IndividualTelephone
    {
        public Guid IndividualTelephoneId { get; set; }
        public Guid TelephoneId { get; set; }
        public Guid IndividualId { get; set; }
        public Individual Individual { get; set; }
        public Telephone Telephone { get; set; }
    }
}
