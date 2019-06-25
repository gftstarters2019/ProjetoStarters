using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Beneficiaries.WebAPI.ViewModels
{
    public class MobileDeviceViewModel
    {
        public Guid BeneficiaryId { get; set; }
        [MaxLength(15)]
        public string MobileDeviceBrand { get; set; }
        [MaxLength(20)]
        public string MobileDeviceModel { get; set; }
        [MaxLength(40)]
        public string MobileDeviceSerialNumber { get; set; }
        public DateTime MobileDeviceManufactoringYear { get; set; }
        public MobileDeviceType MobileDeviceType { get; set; }
        public double MobileDeviceInvoiceValue { get; set; }
    }
}
