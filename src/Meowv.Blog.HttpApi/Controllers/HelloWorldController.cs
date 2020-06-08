using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.Domain.Shared;
using Meowv.Blog.HelloWorld;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

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
            return  _helloWorldService.HelloWorld();
        }
    }
}
