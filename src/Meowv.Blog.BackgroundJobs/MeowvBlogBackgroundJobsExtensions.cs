using System;
using System.Collections.Generic;
using System.Text;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Meowv.Blog.BackgroundJobs.Hangfire;
using Meowv.Blog.BackgroundJobs.Jobs.WallpaperJob;

namespace Meowv.Blog.BackgroundJobs
{
    public static class MeowvBlogBackgroundJobsExtensions
    {
        public static void UseHangfireTest(this IServiceProvider service)
        {
            var job = service.GetService<HangfireTestJob>();
            RecurringJob.AddOrUpdate("定时任务测试", () => job.ExecuteAsync(), CronType.Minute());
        }
        
        public static void UseWallpaperJob(this IServiceProvider service)
        {
            var job=service.GetService<WallpaperJob>();
            RecurringJob.AddOrUpdate("定时任务测试1",()=>job.ExecuteAsync(),CronType.Hour(1,3));
        }

    }
}
