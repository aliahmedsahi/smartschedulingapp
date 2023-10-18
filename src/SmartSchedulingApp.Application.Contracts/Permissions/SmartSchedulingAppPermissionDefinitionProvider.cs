using SmartSchedulingApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SmartSchedulingApp.Permissions;

public class SmartSchedulingAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SmartSchedulingAppPermissions.GroupName);
        //Define your own permissions here. Example:
        myGroup.AddPermission(SmartSchedulingAppPermissions.DoctorPermission, L("Permission:Doctor"));
        myGroup.AddPermission(SmartSchedulingAppPermissions.StaffPermission, L("Permission:Staff"));
    }


    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartSchedulingAppResource>(name);
    }
}
