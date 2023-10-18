using SmartSchedulingApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SmartSchedulingApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SmartSchedulingAppController : AbpControllerBase
{
    protected SmartSchedulingAppController()
    {
        LocalizationResource = typeof(SmartSchedulingAppResource);
    }
}
