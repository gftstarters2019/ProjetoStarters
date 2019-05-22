using Backend.Core.Enums;
using Backend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Models
{
    public class MobileDevice : IBeneficiary
    {
        public Guid MobileDeviceId { get; set; }
        public Guid BeneficiaryId { get; set; }
        //public IBeneficiary Beneficiary { get; set; }
        public string MobileDeviceBrand { get; set; }
        public string MobileDeviceModel { get; set; }
        public string MobileDeviceSerialNumber { get; set; }
        public DateTime MobileDeviceManufactoringYear { get; set; }
        public MobileDeviceType MobileDeviceType { get; set; }
        public double MobileDeviceInvoiceValue { get; set; }
    }
}
