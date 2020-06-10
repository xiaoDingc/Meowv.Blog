﻿using Meowv.Blog.HelloWorld;
using Microsoft.AspNetCore.Mvc;
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
            return  _helloWorldService.HelloWorld();
        }
    }
}
