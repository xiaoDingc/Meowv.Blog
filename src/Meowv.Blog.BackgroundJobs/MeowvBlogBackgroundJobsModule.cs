using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.SqlServer;
using Meowv.Blog.Domain.Configurations;
using Meowv.Blog.Domain.Shared;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Modularity;

namespace Meowv.Blog.BackgroundJobs
{
    [DependsOn(typeof(AbpBackgroundJobsHangfireModule)
        )]
    public class MeowvBlogBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHangfire(config =>
            {
                config.UseStorage(
                    new SqlServerStorage(AppSettings.ConnectionStrings,
                        new SqlServerStorageOptions
                        {
                            SchemaName = MeowvBlogConsts.DbTablePrefix + "hangfire"
                        })
                    );
            });

        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseHangfireServer();

            app.UseHangfireDashboard(options: new DashboardOptions()
            {
                Authorization = new[]
                   {
                       new BasicAuthAuthorizationFilter(new BasicAuthAuthorizationFilterOptions
                       {
                            //RequireSsl = false,
                            //SslRedirect =false,
                            //LoginCaseSensitive=false,
                            Users=new []
                            {
                              new BasicAuthAuthorizationUser()
                              {
                                   Login= AppSettings.HangFire.Login,
                                   PasswordClear = AppSettings.HangFire.Password,
                              }
                            },
                       }),
                   },
                DashboardTitle = "任务调度中心"
            });
            var service = context.ServiceProvider;
            //service.UseHangfireTest();
            service.UseWallpaperJob();
        }
    }
}
