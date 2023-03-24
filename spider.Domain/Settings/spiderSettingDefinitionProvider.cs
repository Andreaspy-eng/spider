using Volo.Abp.Settings;

namespace spider.Settings;

public class spiderSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(spiderSettings.MySetting1));
    }
}
