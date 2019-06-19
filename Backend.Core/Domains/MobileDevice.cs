using Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Core.Domains
{
    public class MobileDevice : Beneficiary
    {
        public string MobileDeviceBrand { get; set; }
        public string MobileDeviceModel { get; set; }
        public string MobileDeviceSerialNumber { get; set; }
        public DateTime MobileDeviceManufactoringYear { get; set; }
        public MobileDeviceType MobileDeviceType { get; set; }
        public double MobileDeviceInvoiceValue { get; set; }
    }
}
