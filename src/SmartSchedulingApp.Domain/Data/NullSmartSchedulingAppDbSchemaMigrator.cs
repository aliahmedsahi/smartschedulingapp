using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SmartSchedulingApp.Data;

/* This is used if database provider does't define
 * ISmartSchedulingAppDbSchemaMigrator implementation.
 */
public class NullSmartSchedulingAppDbSchemaMigrator : ISmartSchedulingAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
