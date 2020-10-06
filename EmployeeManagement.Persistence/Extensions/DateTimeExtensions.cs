using System;

namespace EmployeeManagement.Persistence.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToCustomString(this DateTime dateTime) => dateTime.ToString("d MMM yyyy");
    }
}
