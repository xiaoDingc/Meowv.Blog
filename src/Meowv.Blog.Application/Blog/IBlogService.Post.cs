using Meowv.Blog.Application.Contracts;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Application.Contracts.Blog.Params;
using Meowv.Blog.ToolKits.Base.Paged;

namespace Meowv.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 根据URL获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url);

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync(PagingInput input);

        /// <summary>
        /// 通过标签名称查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByTagAsync(string name);

        /// <summary>
        /// 通过分类名称查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //  Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByCategoryAsync(string name);

        Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input);


        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> InsertPostAsync(EditPostInput input);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdatePostAsync(int id, EditPostInput input);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult> DeletePostAsync(int id);
    }
}