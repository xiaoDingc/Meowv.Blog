using Meowv.Blog.HelloWorld;
using Microsoft.AspNetCore.Mvc;
using System;
using Volo.Abp.AspNetCore.Mvc;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName =Grouping.GroupName_v3)]
    public class HelloWorldController:AbpController
    {
        private  readonly  IHelloWorldService _helloWorldService;

        public HelloWorldController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        [HttpGet]
        public  string HelloWorld()
        {
            var a=1;
            var b=0;
            var c=a/b;
            return  _helloWorldService.HelloWorld();
        }

        [HttpGet]
        [Route("Exception")]
        public string Exception()
        {
            throw new NotImplementedException("这是一个未实现的异常接口");
        }
    }
}
