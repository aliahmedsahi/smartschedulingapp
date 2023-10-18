using System;
using System.Collections.Generic;
using System.Text;
using SmartSchedulingApp.Localization;
using Volo.Abp.Application.Services;

namespace SmartSchedulingApp;

/* Inherit your application services from this class.
 */
public abstract class SmartSchedulingAppAppService : ApplicationService
{
    protected SmartSchedulingAppAppService()
    {
        LocalizationResource = typeof(SmartSchedulingAppResource);
    }
}
