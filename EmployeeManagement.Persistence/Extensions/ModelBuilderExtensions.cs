using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq;

namespace EmployeeManagement.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureDbFunctions(this ModelBuilder builder)
        {
            var mi = typeof(DateTimeExtensions).GetMethod(nameof(DateTimeExtensions.ToCustomString));

            builder.HasDbFunction(mi, b => b.HasTranslation(e =>
            {
                var args = new[]
                {
                    e.First(),
                    new SqlFragmentExpression("'d MMM yyyy'"),
                    new SqlFragmentExpression("'iv'")
                };
                return SqlFunctionExpression.Create("FORMAT", args, typeof(string), null);
            }));
        }
    }
}
