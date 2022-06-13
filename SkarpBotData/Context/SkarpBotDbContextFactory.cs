using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SkarpBot.Data.Context
{
    public class SkarpBotDbContextFactory : IDesignTimeDbContextFactory<SkarpBotDbContext>
    {
        public SkarpBotDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var optionBuilder = new DbContextOptionsBuilder()
                .UseMySql(configuration.GetConnectionString("Default"),
                new MySqlServerVersion(new Version(8, 0, 29)));

            return new SkarpBotDbContext(optionBuilder.Options);
        }
    }
}
