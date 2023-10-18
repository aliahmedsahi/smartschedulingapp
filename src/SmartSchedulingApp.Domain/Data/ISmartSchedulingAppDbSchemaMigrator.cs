using System.Threading.Tasks;

namespace SmartSchedulingApp.Data;

public interface ISmartSchedulingAppDbSchemaMigrator
{
    Task MigrateAsync();
}
