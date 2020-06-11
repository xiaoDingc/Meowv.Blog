using Meowv.Blog.ToolKits.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meowv.Blog.HttpApi.Hosting.Filters
{
    public class MeowvBlogExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
           // 日志记录
            LoggerHelper.WriteToFile($"{context.HttpContext.Request.Path}|{context.Exception.Message}", context.Exception);
        }
    }
}
