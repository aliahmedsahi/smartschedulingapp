using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmartSchedulingApp.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class SmartSchedulingAppDbContextFactory : IDesignTimeDbContextFactory<SmartSchedulingAppDbContext>
{
    public SmartSchedulingAppDbContext CreateDbContext(string[] args)
    {
        SmartSchedulingAppEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SmartSchedulingAppDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"), x=>x.UseDateOnlyTimeOnly());

        return new SmartSchedulingAppDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SmartSchedulingApp.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
