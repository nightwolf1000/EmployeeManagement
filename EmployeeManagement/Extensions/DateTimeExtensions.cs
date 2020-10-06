using System;

namespace EmployeeManagement.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToCustomString(this DateTime dateTime) => dateTime.ToString("d MMM yyyy");

        public static int ComputeAgeInYears(this DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
