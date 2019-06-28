using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IVehicleValidator
    {
        bool IsValid(VehicleDomain vehicle);
    }
}
