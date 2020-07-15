using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    public class QueryPostForAdminDto : QueryPostDto
    {
        /// <summary>
        /// Posts
        /// </summary>
        public IEnumerable<PostBriefForAdminDto> Posts { get; set; }

    }
}
