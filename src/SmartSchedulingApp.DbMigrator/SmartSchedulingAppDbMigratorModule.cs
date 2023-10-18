using SmartSchedulingApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SmartSchedulingApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SmartSchedulingAppEntityFrameworkCoreModule),
    typeof(SmartSchedulingAppApplicationContractsModule)
    )]
public class SmartSchedulingAppDbMigratorModule : AbpModule
{
}
