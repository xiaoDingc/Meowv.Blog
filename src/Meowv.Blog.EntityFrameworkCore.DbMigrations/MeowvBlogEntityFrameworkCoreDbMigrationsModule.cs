using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Meowv.Blog
{
    [DependsOn((typeof(MeowvBlogFrameworkCoreModule)))]
   public class MeowvBlogEntityFrameworkCoreDbMigrationsModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<MeowvBlogMigrationsDbContext>();
           
            //base.ConfigureServices(context);
        }
    }
}
