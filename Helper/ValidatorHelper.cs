using System.Text.RegularExpressions;

namespace PIS.API.Helper
{
    public static class ValidatorHelper
    {
        public static bool AlphaNumericOnly(string value)
        {
            var result = Regex.IsMatch(value, @"^[0-9a-zA-Z ]+$");
            
            if (result)
                return true;

            return false;
        }
        
        public static bool NumericOnly(string value)
        {
            var result = Regex.IsMatch(value, @"^[0-9]+$");
            
            if (result)
                return true;

            return false;
        }
        
        
    }
}