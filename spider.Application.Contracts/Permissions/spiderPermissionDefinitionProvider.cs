using spider.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace spider.Permissions;

public class spiderPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(spiderPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(spiderPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<spiderResource>(name);
    }
}
