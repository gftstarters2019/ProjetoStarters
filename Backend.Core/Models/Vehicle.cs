using Backend.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Models
{
    public class Vehicle : Beneficiary
    {
        public Guid VehicleId { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleModel { get; set; }
        public DateTime VehicleManufactoringYear { get; set; }
        public Color VehicleColor { get; set; }
        public DateTime VehicleModelYear { get; set; }
        public string VehicleChassisNumber { get; set; }
        public int VehicleCurrentMileage { get; set; }
        public double VehicleCurrentFipeValue { get; set; }
        public bool VehicleDoneInspection { get; set; }
    }
}
