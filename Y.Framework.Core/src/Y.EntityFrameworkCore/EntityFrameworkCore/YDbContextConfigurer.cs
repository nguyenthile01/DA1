using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Y.EntityFrameworkCore
{
    public static class YDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<YDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<YDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
