using spider.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace spider.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(spiderEntityFrameworkCoreModule),
    typeof(spiderApplicationContractsModule)
    )]
public class spiderDbMigratorModule : AbpModule
{

}
