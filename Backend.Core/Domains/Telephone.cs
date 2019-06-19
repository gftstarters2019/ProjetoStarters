using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class Telephone
    {
        public Guid TelephoneId { get; set; }
        public string TelephoneNumber { get; set; }
        public TelephoneType TelephoneType { get; set; }
    }
}
