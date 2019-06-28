using Backend.Core.Domains;

namespace Backend.Services.Validators.Contracts
{
    public interface IMobileDeviceValidator
    {
        bool IsValid(MobileDeviceDomain mobileDevice);
    }
}
