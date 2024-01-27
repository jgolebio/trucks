using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Trucks.Infrastructure.Sql.Extensions;

internal static class DapperExtensions
{
    public static SqlConnection GetSqlConnection(this TrucksDbContext context) =>
        new SqlConnection(context.Database.GetConnectionString());
}
