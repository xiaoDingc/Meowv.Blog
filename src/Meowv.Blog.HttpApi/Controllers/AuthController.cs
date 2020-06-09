﻿using Meowv.Blog.Application.Authorize;
using Meowv.Blog.Domain.Shared;
using Meowv.Blog.ToolKits.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Meowv.Blog.HttpApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName =Grouping.GroupName_v4)]
    public class AuthController:AbpController
    {
        private readonly IAuthorizeService _authorizeService;

        public AuthController(IAuthorizeService authorizeService)
        {
            _authorizeService=authorizeService;
        }

         /// <summary>
        ///获取AccessToken
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("access_token")]
        public async Task<ServiceResult<string>> GetAccessTokenAsync(string code)
        {
            return await _authorizeService.GetAccessTokenAsync(code);
        }

        /// <summary>
        ///  获取登录地址(GitHub)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("url")]
        public async Task<ServiceResult<string>> GetLoginAddressAsync()
        {
            return await _authorizeService.GetLoginAddressAsync();
        }

       
    }
}