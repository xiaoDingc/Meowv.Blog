using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meowv.Blog.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Meowv.Blog.Web
{
    [DependsOn(
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpAutofacModule),
        typeof(MeowvBlogHttpApiModule),
        typeof(MeowvBlogSwaggerModule),
          typeof(MeowvBlogFrameworkCoreModule)
        )]
    public class MeowvBlogHttpApiHostingModule:AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

           app.UseEndpoints(endpoints=>
           {
               endpoints.MapControllers();
           });
        }
    }
}
