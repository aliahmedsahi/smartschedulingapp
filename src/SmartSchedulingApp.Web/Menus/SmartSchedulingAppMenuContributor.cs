using System.Threading.Tasks;
using SmartSchedulingApp.Localization;
using SmartSchedulingApp.MultiTenancy;
using SmartSchedulingApp.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace SmartSchedulingApp.Web.Menus;

public class SmartSchedulingAppMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<SmartSchedulingAppResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                SmartSchedulingAppMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Doctors",
                l["Menu:Doctors"],
                icon: "fa fa-user-md",
                url: "/Doctors",
                order: 1,
                requiredPermissionName: SmartSchedulingAppPermissions.DoctorPermission
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Staff",
                l["Menu:Staff"],
                icon: "fa fa-users",
                url: "/Staffs",
                order: 2,
                requiredPermissionName: SmartSchedulingAppPermissions.StaffPermission
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
