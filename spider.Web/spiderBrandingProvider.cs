using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace spider.Web;

[Dependency(ReplaceServices = true)]
public class spiderBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "=Логистика=";
}
