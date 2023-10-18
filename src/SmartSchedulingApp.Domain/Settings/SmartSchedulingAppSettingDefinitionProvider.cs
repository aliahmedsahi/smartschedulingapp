using Volo.Abp.Settings;

namespace SmartSchedulingApp.Settings;

public class SmartSchedulingAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SmartSchedulingAppSettings.MySetting1));
    }
}
