using Backend.Application.Interfaces;
using Backend.Core.Enums;
using Backend.Core.Models;
using System;

namespace Backend.Application.Factories
{
    public class vehicleFactory : IFactory<Vehicle>
    {
        public Vehicle Create(Guid vehicleId, string vehicleBrand, string vehicleModel, DateTime vehicleManufacturingYear, Color vehicleColor, DateTime vehicleModelyear, string vehicleChassisNumber, short vehicleCurrentMileage, double vehicleCurrentFipeValue, bool vehicleDoneInspection)
        {
            var vehicle = new Vehicle();
            vehicle.VehicleId = Guid.NewGuid();
            vehicle.VehicleBrand = vehicleBrand;
            vehicle.VehicleModel = vehicleModel;
            vehicle.VehicleManufactoringYear = vehicleManufacturingYear;
            vehicle.VehicleColor = vehicleColor;
            vehicle.VehicleModelYear = vehicleModelyear;
            vehicle.VehicleChassisNumber = vehicleChassisNumber;
            vehicle.VehicleCurrentMileage = vehicleCurrentMileage;
            vehicle.VehicleCurrentFipeValue = vehicleCurrentFipeValue;
            vehicle.VehicleDoneInspection = vehicleDoneInspection;

            return vehicle;
        }
        public Vehicle Create()
        {
           return new Vehicle();
        }
    }
}