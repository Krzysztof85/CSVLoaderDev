using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSVLoader
{
    public class Validator : IValidator
    {
        private IEnumerable<string> _ccyCodes;
        public Validator()
        {
            _ccyCodes = GetCCYCodes();
        }

        public bool Validate(string[] values, out string message)
        {
            message = string.Empty;

            if (values.Length != 4)
            {
                message = "Invalid number of values";
            }
            else if (values.Any(x => string.IsNullOrEmpty(x)))
            {
                message = "Field cannot be empty";
            }
            else if (!_ccyCodes.Contains(values[2]))
            {
                message = "Code must be a valid ISO 4217 currency code";
            }
            else if (!IsNumber(values[3]))
            {
                message = "Value must be a valid number";
            }
            else
            {
                return true;
            }
            return false;
        }


        private bool IsNumber(string value)
        {
            double result = 0;
            return double.TryParse(value, out result);
        }

        private IEnumerable<string> GetCCYCodes()
        {
            return CultureInfo
                 .GetCultures(CultureTypes.AllCultures)
                 .Where(c => !c.IsNeutralCulture)
                 .Select(culture =>
                 {
                     try
                     {
                         return new RegionInfo(culture.LCID);
                     }
                     catch
                     {
                         return null;
                     }
                 }).Where(ri => ri != null).Select(ri => ri.ISOCurrencySymbol).Distinct().ToList();
        }
    }
}
