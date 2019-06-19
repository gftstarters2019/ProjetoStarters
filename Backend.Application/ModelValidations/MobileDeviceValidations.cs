using Backend.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ModelValidations
{
    public static class MobileDeviceValidations
    {
        /// <summary>
        /// Verifies if Mobile Device is valid
        /// </summary>
        /// <param name="mobileDevice">Mobile Device to be verified</param>
        /// <returns>If Mobile Device is valid</returns>
        public static bool MobileDeviceIsValid(MobileDeviceEntity mobileDevice)
        {
            if (!ValidationsHelper.DateIsValid(mobileDevice.MobileDeviceManufactoringYear))
                return false;

            if (mobileDevice.MobileDeviceInvoiceValue <= 0)
                return false;

            return true;
        }
    }
}
