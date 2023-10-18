using SmartSchedulingApp.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace SmartSchedulingApp.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class SmartSchedulingAppPageModel : AbpPageModel
{
    protected SmartSchedulingAppPageModel()
    {
        LocalizationResourceType = typeof(SmartSchedulingAppResource);
    }
}
