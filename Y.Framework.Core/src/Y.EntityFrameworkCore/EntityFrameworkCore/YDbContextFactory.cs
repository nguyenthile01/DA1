using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Y.Configuration;
using Y.Web;

namespace Y.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class YDbContextFactory : IDesignTimeDbContextFactory<YDbContext>
    {
        public YDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<YDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            YDbContextConfigurer.Configure(builder, configuration.GetConnectionString(YConsts.ConnectionStringName));

            return new YDbContext(builder.Options);
        }
    }
}
