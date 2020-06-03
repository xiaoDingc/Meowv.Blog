using Abp.Modules;
using Castle.Windsor.Installer;
using Volo.Abp.Caching;

namespace Meowv.Blog.Application.Caching
{
    [DependsOn(typeof(AbpCachingModule))]
    public class MeowvBlogApplicationCachingModule:AbpModule
    {
     
    }
}