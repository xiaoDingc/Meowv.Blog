using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.BackgroundJobs.Jobs
{
    public interface IBackgroundJob:ITransientDependency
    {
        /// <summary>
        /// 异步执行任务
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
    }
}
