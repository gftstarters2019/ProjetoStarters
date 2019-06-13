using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.ModelValidations
{
    public static class ValidationsHelper
    {
        /// <summary>
        /// Verifies if date is not future date
        /// </summary>
        /// <returns>If date is valid</returns>
        public static bool DateIsValid(DateTime date)
        {
            return date != null ? date < DateTime.Today : false;
        }
    }
}
