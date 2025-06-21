using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Blaze.Repository
{
    public class BlazeContextFactory : IDesignTimeDbContextFactory<BlazeContext>
    {
        public BlazeContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BlazeContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");


            optionsBuilder.UseNpgsql(connectionString);

            return new BlazeContext(optionsBuilder.Options);
        }
    }
}
