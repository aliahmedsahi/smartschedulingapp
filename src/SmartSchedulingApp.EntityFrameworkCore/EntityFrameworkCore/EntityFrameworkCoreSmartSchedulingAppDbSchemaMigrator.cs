using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartSchedulingApp.Data;
using Volo.Abp.DependencyInjection;

namespace SmartSchedulingApp.EntityFrameworkCore;

public class EntityFrameworkCoreSmartSchedulingAppDbSchemaMigrator
    : ISmartSchedulingAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSmartSchedulingAppDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the SmartSchedulingAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SmartSchedulingAppDbContext>()
            .Database
            .MigrateAsync();
    }
}
