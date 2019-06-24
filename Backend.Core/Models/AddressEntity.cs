using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class AddressEntity
    {
        [Key]
        public Guid AddressId { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressNeighborhood { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressCountry { get; set; }
        public string AddressZipCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
