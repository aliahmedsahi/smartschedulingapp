using Volo.Abp.Modularity;

namespace SmartSchedulingApp;

[DependsOn(
    typeof(SmartSchedulingAppApplicationModule),
    typeof(SmartSchedulingAppDomainTestModule)
    )]
public class SmartSchedulingAppApplicationTestModule : AbpModule
{

}
