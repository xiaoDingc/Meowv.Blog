﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;

namespace Meowv.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync();
    }
}