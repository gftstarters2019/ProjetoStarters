using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class TelephoneEntity
    {
        [Key]
        public Guid TelephoneId { get; set; }
        public string TelephoneNumber { get; set; }
        public TelephoneType TelephoneType { get; set; }
    }
}
