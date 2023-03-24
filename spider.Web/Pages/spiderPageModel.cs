using spider.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace spider.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class spiderPageModel : AbpPageModel
{
    protected spiderPageModel()
    {
        LocalizationResourceType = typeof(spiderResource);
    }
}
