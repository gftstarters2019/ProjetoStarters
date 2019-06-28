using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IVehicleValidator
    {
        List<string> IsValid(VehicleDomain vehicle);
    }
}
