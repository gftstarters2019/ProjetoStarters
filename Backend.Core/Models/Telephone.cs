using Backend.Core.Enums;
using System;

namespace Backend.Core.Models
{
    public class Telephone
    {
        public Guid TelephoneId { get; set; }
        public string TelephoneNumber { get; set; }
        public TelephoneType TelephoneType { get; set; }
    }
}
