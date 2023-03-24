using System;
using System.Collections.Generic;
using System.Text;
using spider.Localization;
using Volo.Abp.Application.Services;

namespace spider;

/* Inherit your application services from this class.
 */
public abstract class spiderAppService : ApplicationService
{
    protected spiderAppService()
    {
        LocalizationResource = typeof(spiderResource);
    }
}
