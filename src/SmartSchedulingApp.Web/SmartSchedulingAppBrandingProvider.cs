using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace SmartSchedulingApp.Web;

[Dependency(ReplaceServices = true)]
public class SmartSchedulingAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SmartSchedulingApp";
}
