using System.ComponentModel.DataAnnotations;
using Meowv.Blog.Application.Blog;
using Meowv.Blog.Application.Contracts.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Base.Paged;
using Volo.Abp.AspNetCore.Mvc;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;
using Microsoft.AspNetCore.Authorization;

namespace Meowv.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public class BlogController : AbpController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }


        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts")]
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync([FromQuery]PagingInput input)
        {
            return await _blogService.QueryPostsAsync(input);
        }

        [HttpGet]
        [Route("post")]
        public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url)
        {
            return await _blogService.GetPostDetailAsync(url);
        }
    }
}