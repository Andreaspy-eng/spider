using System.Threading.Tasks;

namespace spider.Data;

public interface IspiderDbSchemaMigrator
{
    Task MigrateAsync();
}
