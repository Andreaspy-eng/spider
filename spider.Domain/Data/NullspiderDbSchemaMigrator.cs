using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace spider.Data;

/* This is used if database provider does't define
 * IspiderDbSchemaMigrator implementation.
 */
public class NullspiderDbSchemaMigrator : IspiderDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
