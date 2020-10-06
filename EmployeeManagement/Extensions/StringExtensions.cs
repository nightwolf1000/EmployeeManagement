using System;
using System.Globalization;

namespace EmployeeManagement.Extensions
{
    public static class StringExtensions
    {
        public static string TrimAndNullIfWhiteSpace(this string value) =>
           string.IsNullOrWhiteSpace(value)
           ? null
           : value.Trim();

        public static bool IsValidDateTime(this string value)
        {
            return DateTime.TryParseExact(value,
                   "d MMM yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out _);
        }
        
        public static DateTime ToDateTime(this string value)
        {
            return DateTime.ParseExact(value, "d MMM yyyy",
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None);
        }
    }
}
