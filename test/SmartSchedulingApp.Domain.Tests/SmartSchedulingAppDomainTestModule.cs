using SmartSchedulingApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SmartSchedulingApp;

[DependsOn(
    typeof(SmartSchedulingAppEntityFrameworkCoreTestModule)
    )]
public class SmartSchedulingAppDomainTestModule : AbpModule
{

}
