using System;
using Backend.Core.Enums;

namespace Beneficiaries.WebAPI.ViewModels
{
    public class VehicleViewModel
    {
        public Guid individualId { get; set; }
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
