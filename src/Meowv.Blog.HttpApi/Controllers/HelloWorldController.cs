using System;
using System.Collections.Generic;
using System.Text;
using Meowv.Blog.HelloWorld;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Meowv.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
