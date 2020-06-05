using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.ToolKits.Base.Enum;

namespace Meowv.Blog.ToolKits.Base
{
    public class ServiceResult
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public ServiceResultCodeEnum Code { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public bool Success => Code == ServiceResultCodeEnum.Succeed;

        /// <summary>
        /// 时间戳(毫秒)
        /// </summary>
        public long Timestamp => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public void IsSuccess(string message = "")
        {
            Message = message;
            Code = ServiceResultCodeEnum.Succeed;
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public void IsFailed(string message = "")
        {
            Message = message;
            Code = ServiceResultCodeEnum.Failed;
        }

        public void IsFailed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = ServiceResultCodeEnum.Failed;
        }
    }
}
