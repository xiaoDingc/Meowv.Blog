using System;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Meowv.Blog.Domain.Shared
{
    [DependsOn(typeof(AbpIdentityDomainModule))]
    public class MeowvBlogDomainSharedModule:AbpModule
    {

    }
}
