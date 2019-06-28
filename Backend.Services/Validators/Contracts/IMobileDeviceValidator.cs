using Backend.Core.Domains;
using System.Collections.Generic;

namespace Backend.Services.Validators.Contracts
{
    public interface IMobileDeviceValidator
    {
        List<string> IsValid(MobileDeviceDomain mobileDevice);
    }
}
