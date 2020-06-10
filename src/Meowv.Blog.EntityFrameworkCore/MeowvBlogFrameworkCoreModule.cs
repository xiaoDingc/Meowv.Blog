using Meowv.Blog.Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace Meowv.Blog.EntityFrameworkCore
{
    [DependsOn(
        typeof(MeowvBlogDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
        )]
    public class MeowvBlogFrameworkCoreModule : AbpModule
    {


        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MeowvBlogDbContext>(opt=>
            {
                opt.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(opt=>
            {
                switch (AppSettings.EnableDb)
                {
                    case "MySQL":
                        opt.UseMySQL();
                        break;
                    case "SqlServer":
                        opt.UseSqlServer();
                        break;
                    case "PostgreSql":
                        opt.UseNpgsql();
                        break;
                    case "Sqlite":
                        opt.UseSqlite();
                        break;
                    default:
                        opt.UseSqlServer();
                        break;
                }
            });
        }
    }
}
