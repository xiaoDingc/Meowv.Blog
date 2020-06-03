using Microsoft.AspNetCore.Builder;
using System;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Meowv.Blog.Swagger
{
    [DependsOn(
        typeof(MeowvBlogDomainModule)
        )]
    public class MeowvBlogSwaggerModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
          context.Services.AddSwagger();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
           context.GetApplicationBuilder().UseSwagger().UseSwaggerUI();
        }
    }
}
