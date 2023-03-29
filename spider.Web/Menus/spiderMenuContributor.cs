using System.Threading.Tasks;
using spider.Localization;
using spider.MultiTenancy;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace spider.Web.Menus;

public class spiderMenuContributor : IMenuContributor
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
        var l = context.GetLocalizer<spiderResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                spiderMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home"
            )
        );

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                spiderMenus.Home,
                l["Handbook"],
                icon: "fas fa-book"
            ).AddItem(
        new ApplicationMenuItem(
            "spider.Products",
            l["Products"],
            url: "/Products"
        )
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
