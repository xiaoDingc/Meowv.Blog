using Meowv.Blog.Domain.Shared.Enum;

namespace Meowv.Blog.Application.Contracts.HotNews
{
    public class HotNewsJobItem<T>
    {
        /// <summary>
        /// 泛型结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        ///  来源
        /// </summary>
        public HotNewsEnum Source { get; set; }


    }
}