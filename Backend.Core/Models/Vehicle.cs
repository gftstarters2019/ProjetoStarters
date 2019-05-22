using Backend.Core.Enums;
using Backend.Core.Interfaces;
using System;

namespace Backend.Core.Models
{
    public class Vehicle : IBeneficiary
    {
        public Guid VehicleId { get; set; }
        public Guid BeneficiaryId { get; set; }
        //public IBeneficiary Beneficiary { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime VehicleManufactoringYear { get; set; }
        public Color VehicleColor { get; set; }
        public DateTime VehicleModelYear { get; set; }
        public string VehicleChassisNumber { get; set; }
        public Int16 VehicleCurrentMileage { get; set; }
        public double VehicleCurrentFipeValue { get; set; }
        public bool VehicleDoneInspection { get; set; }
    }
}
