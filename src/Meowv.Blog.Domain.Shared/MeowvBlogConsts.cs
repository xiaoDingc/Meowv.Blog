namespace Meowv.Blog.Domain.Shared
{
    /// <summary>
    /// 全局常量
    /// </summary>
    public class MeowvBlogConsts
    {
        /// <summary>
        /// 数据库表前缀
        /// </summary>
        public const string DbTablePrefix = "meowv_";

        /// <summary>
        /// 分组
        /// </summary>
        public static class Grouping
        {
            /// <summary>
            /// 博客前台接口组
            /// </summary>
            public const string GroupName_v1 = "v1";

            /// <summary>
            /// 博客后台接口组
            /// </summary>
            public const string GroupName_v2 = "v2";

            /// <summary>
            /// 其他通用接口组
            /// </summary>
            public const string GroupName_v3 = "v3";

            /// <summary>
            /// JWT授权接口组
            /// </summary>
            public const string GroupName_v4 = "v4";
        }

        public static class CachePrefix
        {
            public const string Authorize = "Authorize";

            public const string Blog = "Blog";

            public const string Blog_Post = Blog + ":Post";

            public const string Blog_Tag = Blog + ":Tag";

            public const string Blog_Category = Blog + ":Category";

            public const string Blog_FriendLink = Blog + ":FriendLink";
        }
    }
}
