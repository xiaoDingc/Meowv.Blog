namespace Meowv.Blog.Application.Contracts.Blog
{
    public class QueryTagDto:TagDto
    {
        /// <summary>
        /// 数量
        /// </summary>
        public  int Count { get; set; }
    }
}