using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace Meowv.Blog
{
    [DependsOn(typeof(AbpIdentityDomainModule))]
    public class MeowvBlogDomainModule : AbpModule
    {

    }
}
