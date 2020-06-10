using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.ToolKits.Base.Enum;

namespace Meowv.Blog.ToolKits.Base
{
    public class ServiceResult<T> : ServiceResult where T : class
    {
        public T Result { get; set; }

        public void IsSuccess(T result = null, string message = "")
        {
            Result = result;
            Code = ServiceResultCodeEnum.Succeed;
            Message = message;
        }
    }
}
