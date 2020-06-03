
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Meowv.Blog
{
    [DependsOn(
        typeof(AbpIdentityHttpApiModule),
        typeof(MeowvBlogApplicationModule)
    )]
    public class MeowvBlogHttpApiModule : AbpModule
    {
        
    }
}
